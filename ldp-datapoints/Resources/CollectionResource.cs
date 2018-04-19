/*
Copyright 2018 T.Spieldenner, DFKI GmbH

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),
to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using LDPDatapoints.Messages;
using LDPDatapoints.Subscriptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF;

namespace LDPDatapoints.Resources
{
    /// <summary>
    /// A Resource that wraps a collection type object. The collection needs to implement the INotifyCollectionChanged
    /// interface, so that Subscriptions are notified about changes in the collection.
    /// </summary>
    /// <typeparam name="T">The collection type that this Resource should represent.</typeparam>
    /// <typeparam name="U">The generic type of the collection elements.</typeparam>
    public class CollectionResource<T, U> : SubscriptionResource<T> where T : ICollection<U>, INotifyCollectionChanged
    {
        private bool collectionHasPropertyElements = false;

        public CollectionResource(T value, string route) : base(value, route)
        {
            _value = value;
            _value.CollectionChanged += (o, e) => NotifySubscriptions(o, e);
            collectionHasPropertyElements = typeof(U).IsAssignableFrom(typeof(INotifyPropertyChanged));
        }

        private void handleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var _valueAsCollection = _value as Collection<U>;
            if (collectionHasPropertyElements)
            {
                var count = 0;
                foreach (var i in e.NewItems)
                {
                    var item = i as INotifyPropertyChanged;
                    item.PropertyChanged += (o, p) =>
                    {
                        var index = _valueAsCollection.IndexOf((U)o);
                        handleCollectionItemPropertyChanged((U)o, e.NewStartingIndex + count, p.PropertyName);
                    };
                    count++;
                }
            }
            NotifySubscriptions(sender, e);
        }

        private void handleCollectionItemPropertyChanged(U sender, int index, string property)
        {
            CollectionUpdateMessage<U> m = new CollectionUpdateMessage<U>();
            m.IndexChanged = index;
            m.newObjects = new U[1] { sender };

            m.ObjectAdded = false;
            foreach (ISubscription s in Subscriptions)
            {
                s.SendMessage(m.ToString());
            }
        }

        protected override void NotifySubscriptions(object sender, EventArgs e)
        {
            ObservableCollection<T> o = new ObservableCollection<T>();
            var args = e as NotifyCollectionChangedEventArgs;
            CollectionUpdateMessage<U> m = new CollectionUpdateMessage<U>();
            m.ObjectAdded = args.NewItems.Count > 0;
            if (m.ObjectAdded)
            {
                m.IndexChanged = args.NewStartingIndex;
                U[] newObjects = new U[args.NewItems.Count];
                args.NewItems.CopyTo(newObjects,0);
                m.newObjects = newObjects;
            }
            else
            {
                m.IndexChanged = args.OldStartingIndex;
            }
            string messageString = m.ToString();
            foreach (ISubscription s in Subscriptions)
            {
                s.SendMessage(messageString);
            }
        }

        protected override void onGet(object sender, HttpEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void onPut(object sender, HttpEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void onPost(object sender, HttpEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void onHead(object sender, HttpEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void onPatch(object sender, HttpEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void onDelete(object sender, HttpEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void onConnect(object sender, HttpEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void onOptions(object sender, HttpEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

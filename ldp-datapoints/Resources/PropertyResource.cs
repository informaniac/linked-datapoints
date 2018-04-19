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
using System.ComponentModel;

namespace LDPDatapoints.Resources
{
    class PropertyResource<T> : SubscriptionResource<T> where T : INotifyPropertyChanged
    {
        public PropertyResource(T value, string route) : base(value, route)
        {
            _value.PropertyChanged += NotifySubscriptions;
        }

        protected override void NotifySubscriptions(object sender, EventArgs e)
        {
            var args = e as PropertyChangedEventArgs;
            object newValue = null;
            _value.GetType().GetProperty(args.PropertyName).GetValue(newValue);
            string message = new PropertyUpdateMessage(args.PropertyName, newValue).ToString();
            foreach (ISubscription s in Subscriptions)
            {
                s.SendMessage(message);
            }
        }

        protected override void onConnect(object sender, HttpEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void onDelete(object sender, HttpEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void onGet(object sender, HttpEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void onHead(object sender, HttpEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void onOptions(object sender, HttpEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void onPatch(object sender, HttpEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void onPost(object sender, HttpEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void onPut(object sender, HttpEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

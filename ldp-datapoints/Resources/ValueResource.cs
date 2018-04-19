﻿/*
Copyright 2018 T.Spieldenner, DFKI GmbH

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),
to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using LDPDatapoints.Subscriptions;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using VDS.RDF;
using VDS.RDF.Writing;

namespace LDPDatapoints.Resources
{

    public class ValueResource<T> : SubscriptionResource<T>
    {
        XmlSerializer xmlSerializer;
        XmlWriter xmlWriter;

        public override T Value
        {
            get { return _value; }
            set
            {
                _value = value;
                buildGraph();
                NotifySubscriptions(this, new EventArgs());
            }
        }

        public ValueResource(T value, string route) : base(value, route)
        {
            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            xmlSerializer = new XmlSerializer(typeof(T));
            xmlWriter = XmlWriter.Create(stringWriter);

            Value = value;
        }

        protected override void onGet(object sender, HttpEventArgs e)
        {
            HttpListenerRequest request = e.request;
            HttpListenerResponse response = e.response;
            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            CompressingTurtleWriter ttlWriter = new CompressingTurtleWriter();
            ttlWriter.Save(RDFGraph, stringWriter);
            string graph = stringWriter.ToString();
            response.OutputStream.Write(Encoding.UTF8.GetBytes(graph), 0, graph.Length);
            response.Close();
        }

        protected void buildGraph()
        {
            var graph = new Graph();
            graph.NamespaceMap.AddNamespace("rdf", new Uri("http://www.w3.org/1999/02/22-rdf-syntax-ns#"));            
            var o = graph.CreateLiteralNode(Value.ToString(), new Uri(typeRoute));
            var p = graph.CreateUriNode("rdf:value");
            var s = graph.CreateUriNode(new Uri(route));
            graph.Assert(new Triple(s, p, o));
            RDFGraph = graph;
        }

        protected override void NotifySubscriptions(object sender, EventArgs e)
        {
            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            xmlSerializer.Serialize(stringWriter, _value);
            string valueAsXmlString = stringWriter.ToString();
            foreach (ISubscription s in Subscriptions)
            {
                s.SendMessage(valueAsXmlString);
            }
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

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

using LDPDatapoints;
using LDPDatapoints.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF;
using VDS.RDF.Writing;

namespace LDPDatapoints.Resources
{
    class TypeResource : Resource
    {
        string typeinfo;

        public TypeResource(Type t, string route) : base(route)
        {
            typeinfo = buildTypeInfo(t);
        }

        protected override void onGet(object sender, HttpEventArgs e)
        {
            e.response.OutputStream.Write(Encoding.UTF8.GetBytes(typeinfo), 0, typeinfo.Length);
            e.response.Close();
        }

        protected string buildTypeInfo(Type t)
        {
            // TODO: something else?
            return t.transformTypeToString();
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

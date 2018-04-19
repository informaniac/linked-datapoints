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

namespace LDPDatapoints.Resources
{
    public abstract class Resource
    {
        protected string route { get; }
        protected HttpRequestListener RequestListener { get; }

        public Resource(string route)
        {
            this.route = route;
            RequestListener = new HttpRequestListener(route.TrimEnd('/') + "/");
            RequestListener.OnGet += onGet;
            RequestListener.OnPut += onPut;
            RequestListener.OnPost += onPost;
            RequestListener.OnHead += onHead;
            RequestListener.OnPatch += onPatch;
            RequestListener.OnDelete += onDelete;
            RequestListener.OnConnect += onConnect;
            RequestListener.OnOptions += onOptions;
        }

        protected abstract void onGet(object sender, HttpEventArgs e);
        protected abstract void onPut(object sender, HttpEventArgs e);
        protected abstract void onPost(object sender, HttpEventArgs e);
        protected abstract void onHead(object sender, HttpEventArgs e);
        protected abstract void onPatch(object sender, HttpEventArgs e);
        protected abstract void onDelete(object sender, HttpEventArgs e);
        protected abstract void onConnect(object sender, HttpEventArgs e);
        protected abstract void onOptions(object sender, HttpEventArgs e);
    }
}

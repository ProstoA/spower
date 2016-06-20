using System;
using System.IO;
using System.Text;
using System.Web;

using ProstoA.Data.Presentation;

namespace ProstoA.Data.Providers.Web {
    public class HttpResponseFileViewRender<TView> : IViewRender<TView> where TView : FileView {
        private readonly HttpResponseBase _response;
        private readonly Encoding _contentEncoding;

        public HttpResponseFileViewRender(HttpResponseBase response, Encoding contentEncoding) {
            _response = response;
            _contentEncoding = contentEncoding;
        }

        public void Render(TView view) {
            var fileView = view as FileView;
            if (fileView == null) {
                throw new ArgumentException("View wasn't FileView instance.", nameof(view));
            }

            _response.Clear();
            _response.ContentType = fileView.ContentType;
            _response.HeaderEncoding = _contentEncoding;
            _response.AddHeader("Content-Disposition", "attachment;filename=\"" + HttpUtility.UrlPathEncode(fileView.Title) + "\"");

            var ms = new MemoryStream();
            fileView.Write(ms);

            ms.Position = 0;
            ms.CopyTo(_response.OutputStream);

            _response.End();
        }
    }
}
using System.Web;

using ProstoA.Data.View;

namespace ProstoA.Web.DataView {
    public class HttpContextFileViewRender : IViewRender<FileView, HttpContextBase> {
        public void Render(FileView view, HttpContextBase context) {
            context.Response.Clear();
            context.Response.ContentType = view.ContentType;
            context.Response.HeaderEncoding = context.Request.ContentEncoding;
            context.Response.AddHeader("Content-Disposition", $"attachment;filename=\"{HttpUtility.UrlPathEncode(view.Name+view.FileExtension)}\"");

            view.WriteTo(context.Response.OutputStream);

            context.Response.End();
        }
    }
}
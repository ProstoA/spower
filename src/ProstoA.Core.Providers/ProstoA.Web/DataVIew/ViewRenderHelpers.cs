using System.Web;

using ProstoA.Data.View;

namespace ProstoA.Web.DataView {
    public static class ViewRenderHelpers {
        public static void Render(this IView view, HttpContextBase context) {
            new DefaultViewRenderFactory().Render(view, context);
        }
    }
}
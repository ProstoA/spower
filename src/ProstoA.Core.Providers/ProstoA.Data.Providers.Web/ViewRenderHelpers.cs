using System.Web;

using ProstoA.Data.Presentation;

namespace ProstoA.Data.Providers.Web {
    public static class ViewRenderHelpers {
        public static void Render(this IView view, HttpContextBase context) {
            new DefaultViewRenderFactory().Render(view, context);
        }
    }
}
using System.Web;

using ProstoA.Spower.DependencyInjection;

[assembly: PreApplicationStartMethod(typeof(PreApplicationStartCode), nameof(PreApplicationStartCode.Start))]

namespace ProstoA.Spower.DependencyInjection {
    public class PreApplicationStartCode {
        public static void Start() {
            HttpApplication.RegisterModule(typeof(DependencyInjectionModule));
        }
    }

    internal class DependencyInjectionModule : IHttpModule {
        private static readonly object ContextKey = new object();

        public void Init(HttpApplication application) {
            // Check if this module has been already added
            if (application.Context.Items[ContextKey] != null) {
                return; // already added to the pipeline
            }
            application.Context.Items[ContextKey] = ContextKey;

            application.PostResolveRequestCache += (sender, args) => {
                var app = (HttpApplication)sender;

                if (!app.Request.Url.AbsolutePath.EndsWith(".aspx")) {
                    return; // this is not the Page
                }

                var pageFactory = new DependencyInjectionPageHandlerFactory();
                var absolutePath = HttpUtility.UrlDecode(app.Request.Url.AbsolutePath);
                var virtualPath = VirtualPathUtility.ToAppRelative(absolutePath);
                var serverPath = app.Server.MapPath(virtualPath);
                var httpHandler = pageFactory.GetHandler(app.Context, app.Request.HttpMethod, virtualPath, serverPath);

                // Remap IIS7 to our handler
                app.Context.RemapHandler(httpHandler);
            };
        }

        public void Dispose() {}
    }
}
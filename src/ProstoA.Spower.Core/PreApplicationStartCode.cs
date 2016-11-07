using System.Globalization;
using System.Linq;
using System.Web;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Utilities;

using ProstoA.Spower;
using ProstoA.Spower.DependencyInjection;

[assembly: PreApplicationStartMethod(typeof(PreApplicationStartCode), nameof(PreApplicationStartCode.Start))]

namespace ProstoA.Spower {
    public class PreApplicationStartCode {
        public static void Start() {
            HttpApplication.RegisterModule(typeof(SpowerInjectionModule));
        }
    }

    internal class SpowerInjectionModule : IHttpModule {
        private static volatile bool _applicationStarted = false;
        private static readonly object ContextKey = new object();

        private static SpowerContainer _container;

        public void Init(HttpApplication app) {
            // Check if this module has been already added
            if(app.Context.Items[ContextKey] != null) {
                return; // already added to the pipeline
            }
            app.Context.Items[ContextKey] = ContextKey;


            if (!_applicationStarted) {
                lock (ContextKey) {
                    if (!_applicationStarted) {
                        // this will run only once per application start
                        _container = new SpowerContainer();
                        _applicationStarted = true;
                    }
                }
            }

            app.PostResolveRequestCache += (sender, args) => {
                var absolutePath = HttpUtility.UrlDecode(app.Request.Url.AbsolutePath) ?? string.Empty;

                ISpowerHandlerFactory handlerFactory;
                using (Monitoring.StartScope("SpowerInjectionModule > ISpowerHandlerFactory")) {
                    handlerFactory = _container.GetServices<ISpowerHandlerFactory>()
                        .FirstOrDefault(service =>
                            service.AllowExts.Any(x => absolutePath.EndsWith(x, true, CultureInfo.InvariantCulture))
                        );
                }

                if (handlerFactory == null) {
                    return;
                }

                var virtualPath = VirtualPathUtility.ToAppRelative(absolutePath);
                var serverPath = app.Server.MapPath(virtualPath);

                IHttpHandler httpHandler;
                using (Monitoring.StartScope("SpowerInjectionModule > IHttpHandler")) {
                    httpHandler = handlerFactory.GetHandler(app.Context, app.Request.HttpMethod, virtualPath, serverPath);
                }

                // Remap IIS7 to our handler
                app.Context.RemapHandler(httpHandler);

            };

            app.Error += (sender, args) => {
                var ex = app.Server.GetLastError();

                app.Response.Write("<h2>Global Page Error</h2>\n");
                app.Response.Write("<p>" + ex.Message + "</p>\n" + "<p>" + ex.InnerException?.Message + "</p>\n");
                app.Response.Write("Return to the <a href='Default.aspx'>" + "Default Page</a>\n");

                app.Server.ClearError();
            };
        }

        public void Dispose() {}
    }
}
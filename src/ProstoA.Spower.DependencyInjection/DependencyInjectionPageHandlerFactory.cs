using System;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;

using ProstoA.Spower.DependencyInjection.Properties;

namespace ProstoA.Spower.DependencyInjection {
    public class DependencyInjectionPageHandlerFactory : PageHandlerFactory {
        public override IHttpHandler GetHandler(HttpContext context, string requestType, string virtualPath, string path) {
            try {
                var page = base.GetHandler(context, requestType, virtualPath, path) as Page;
                Debug.Assert(page != null);

                var container = ServiceLocator.For(context);

                page.Init += (sender, args) => {
                    Inject(container, page);
                };

                return page;
            }
            catch (HttpCompileException ex) {
                if (ex.Message.Contains("CS7036:")) {
                    var message = string.Format(Resources.Error_RequiredDefaultConstructor, virtualPath);
                    throw new InvalidOperationException(message);
                }

                throw;
            }
        }

        private static void Inject(IServiceProvider container, Control instance) {
            var baseType = instance?.GetType().BaseType;
            Debug.Assert(baseType != null);

            foreach (Control item in instance.Controls) {
                Inject(container, item);
            }

            if (!(instance is TemplateControl)) {
                return;
            }

            var constructors = baseType.GetConstructors()
                .Select(x => new { method = x, args = x.GetParameters()} )
                .Where(x => x.method.IsPublic && x.args.Length > 0)
                .ToArray();

            if (constructors.Length > 1) {
                var message = string.Format(Resources.Error_CanBeNoMoreThanOne, baseType.FullName);
                throw new InvalidOperationException(message);
            }

            var ctor = constructors.FirstOrDefault();
            if (ctor == null) {
                return; // only Default constructor
            }

            try {
                var args = ctor.args.Select(x => container.GetService(x.ParameterType)).ToArray();
                ctor.method.Invoke(instance, args);
            }
            catch (Exception ex) {
                var message = string.Format(Resources.Error_InitializationFail, baseType.FullName, ex.Message);
                throw new InvalidOperationException(message, ex);
            }
        }
    }
}
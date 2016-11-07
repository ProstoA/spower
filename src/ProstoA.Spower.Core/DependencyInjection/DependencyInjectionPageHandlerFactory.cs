using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;

using Microsoft.Extensions.DependencyInjection;

using ProstoA.Spower.Properties;

namespace ProstoA.Spower.DependencyInjection {
    public class DependencyInjectionPageHandlerFactory : PageHandlerFactory, ISpowerHandlerFactory {
        private readonly IServiceProvider _container;

        public DependencyInjectionPageHandlerFactory(IServiceProvider container) {
            _container = container;
        }

        public string[] AllowExts { get; } = {".aspx"};

        public override IHttpHandler GetHandler(HttpContext context, string requestType, string virtualPath, string path) {
            try {
                var page = base.GetHandler(context, requestType, virtualPath, path) as Page;
                Debug.Assert(page != null);

                page.PreInit += (sender, args) => {
                    InvokeConstructor(_container, page);

                    // Why https://blog.tatham.oddie.com.au/2008/12/20/accessing-aspnet-page-controls-during-preinit/
                    var master = page.Master;
                    while (master != null) { master = master.Master; }

                    Inject(_container, page);
                };

                page.Init += (sender, args) => {
                    //Inject(_container, page);
                };

                page.Error += (sender, args) => {
                    //var ex = page.Server.GetLastError();
                    //ex.Server.ClearError();
                };

                return page;
            }
            catch (HttpCompileException ex) {
                if (ex.Message.Contains("CS7036:")) {
                    var message = Resources.Error_RequireDefaultConstructor_Message(virtualPath);

                    throw new InvalidOperationException(message) {
                        HelpLink = Resources.Error_RequireDefaultConstructor_HelpLink
                    };
                }

                throw;
            }
        }

        private static void Inject(IServiceProvider container, Control instance) {
            foreach (Control item in instance.Controls) {
                Inject(container, item);

                if (item is TemplateControl) {
                    InvokeConstructor(container, item);
                }
            }
        }

        private static void InvokeConstructor(IServiceProvider container, Control instance) {
            var type = instance.GetType();

            var constructor = GetConstructor(type);
            while (constructor == null && type != typeof (Page) && type != typeof (MasterPage) &&
                   type != typeof (UserControl) && type != null) {
                type = type.BaseType;
                constructor = GetConstructor(type);
            }

            if (constructor == null) {
                // only Default constructor
                return;
            }

            try {
                var args =
                    constructor.GetParameters().Select(x => container.GetRequiredService(x.ParameterType)).ToArray();
                constructor.Invoke(instance, args);
            }
            catch (Exception ex) {
                var message = Resources.Error_PageInitializationFail_Message(type.FullName, ex.Message);
                throw new InvalidOperationException(message, ex);
            }
        }

        private static ConstructorInfo GetConstructor(Type type) {
            var constructors = type.GetConstructors()
                .Select(x => new {method = x, args = x.GetParameters()})
                .Where(x => x.method.IsPublic && x.args.Length > 0)
                .Select(x => x.method)
                .ToArray();

            if (constructors.Length > 1) {
                var message = Resources.Error_MustBeOneConstructorWithParameters_Message(type.FullName);
                throw new InvalidOperationException(message) {
                    HelpLink = Resources.Error_MustBeOneConstructorWithParameters_HelpLink
                };
            }

            return constructors.FirstOrDefault();
        }
    }
}
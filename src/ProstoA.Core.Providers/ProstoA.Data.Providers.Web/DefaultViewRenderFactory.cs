using System;
using System.Collections.Generic;
using System.Web;

using ProstoA.Data.Presentation;

namespace ProstoA.Data.Providers.Web {
    public class DefaultViewRenderFactory : IViewRenderFactory {
        private readonly Dictionary<Type, object> _renders = new Dictionary<Type, object> {
            { typeof(FileView), new HttpContextFileViewRender() }
        };

        public void Render<TContext>(IView view, TContext context) {
            var httpContext = context as HttpContextBase;
            if (httpContext == null) {
                throw new ArgumentException($"It was expected '{typeof(HttpContextBase).Name}' instance.", nameof(context));
            }

            var viewType = view.GetType();
            if (!_renders.ContainsKey(viewType)) {
                throw new ArgumentException($"Render for '{viewType.Name}' was not found.", nameof(view));
            }

            var render = _renders[viewType];
            var renderType = typeof(IViewRender<,>).MakeGenericType(viewType, context.GetType());
            renderType.GetMethod("Render").Invoke(render, new object[] { view, httpContext });
        }
    }
}
using System;
using System.Linq;
using System.Web;

using Microsoft.Extensions.DependencyInjection;

namespace ProstoA.Spower.DependencyInjection {
    public static class ServiceLocator {
        private const string ContainerKey = "Nornik.Portal.Core.DependencyInjection";

        public static IServiceProvider For(HttpContext context) {
            if (context.Application.AllKeys.Contains(ContainerKey)) {
                return (IServiceProvider)HttpContext.Current.Application[ContainerKey];
            }

            var container = BuildServiceProvider();
            context.Application.Add(ContainerKey, container);

            return container;
        }

        private static IServiceProvider BuildServiceProvider() {
            var services = new ServiceCollection();
            return services.BuildServiceProvider();
        }
    }
}
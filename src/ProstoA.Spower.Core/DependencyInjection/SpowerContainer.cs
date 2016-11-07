using System;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

namespace ProstoA.Spower.DependencyInjection {
    public class SpowerContainer : IServiceProvider {

        private static readonly Lazy<IServiceProvider> Container = new Lazy<IServiceProvider>(() => {
            var spoverAssemblyName = typeof (SpowerContainer).Assembly.GetName().Name;
            var spowerInterfaceModuleName = typeof (ISpowerModule<>).Name;

            var modules = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.FullName.StartsWith(spoverAssemblyName) || x.GetReferencedAssemblies().Any(xx => xx.Name.StartsWith(spoverAssemblyName)))
                .SelectMany(x => x.ExportedTypes)
                .Select(x => new { Interface = x.GetInterface(spowerInterfaceModuleName), Instance = x })
                .Where(x => x.Interface != null)
                .ToArray();

            var moduleContainer = new ServiceCollection();

            foreach (var module in modules) {
                moduleContainer.AddTransient(module.Interface, module.Instance);
            }

            var moduleResolver = moduleContainer.BuildServiceProvider();

            var services = new SpowerServiceCollection();
            var parameters = new ISpowerServiceCollection[] { services };

            foreach(var module in modules) {
                services.AddTransient(module.Interface, module.Instance);
                var item = moduleResolver.GetService(module.Interface);
                module.Instance.GetMethod("ConfigureServices").Invoke(item, parameters);
            }

            return services.BuildServiceProvider();
        });

        public object GetService(Type serviceType) {
            return Container.Value.GetService(serviceType);
        }
    }
}
using Microsoft.Extensions.DependencyInjection;

using ProstoA.Spower.DependencyInjection;

namespace ProstoA.Spower {
    public class CoreModule : ISpowerModule<CoreModule> {
        public void ConfigureServices(ISpowerServiceCollection services) {
            services.AddTransient<ISpowerHandlerFactory, DependencyInjectionPageHandlerFactory>();
        }
    }
}
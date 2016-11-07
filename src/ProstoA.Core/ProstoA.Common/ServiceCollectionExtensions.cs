using System;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

namespace ProstoA {
    public static class ServiceCollectionExtensions {
        public static void ResolveAndRegister(this IServiceCollection services, Assembly assembly, params Type[] searchType) {
            Func<Type, bool> predicate = x => searchType.Contains(x.GetTypeInfo().IsGenericType ? x.GetGenericTypeDefinition() : x);

            var types = assembly.GetTypes()
                .Select(x => new { Implimentation = x, Services = x.GetTypeInfo().GetInterfaces().Where(predicate) })
                .SelectMany(x => x.Services.Select(xx => new { x.Implimentation, Service = xx }));

            foreach(var item in types) {
                services.AddTransient(item.Service, item.Implimentation);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ProstoA.Delivery.Packaging {
    public class PackageStore {
        private static readonly Dictionary<string, Assembly> _packageCache = new Dictionary<string, Assembly>();
        private static readonly List<IPackageLocator> _packageLocators = new List<IPackageLocator>();

        //public static T GetInstance<T>(string packageName) {
        //    var assembly = LoadPackage(packageName);
        //    var typeName = typeof(T).FullName;
        //    var type = assembly.GetType(typeName, false);
        //    if (type == null) {
        //        throw new TypeLoadException(string.Format("Не удалось найти тип '{0}' в пакете '{1}'.", typeName, packageName));
        //    }

        //    return (T)Activator.CreateInstance(type);
        //}

        //public static string GetStorePath() {
        //    return SPUtility.GetVersionedGenericSetupPath(@"Template\Layouts\ProstoA.SharePoint.System\Packages\", SPUtility.ContextCompatibilityLevel);
        //}

        static PackageStore() {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        public static void RegisterPackageLocator(IPackageLocator locator) {
            _packageLocators.Add(locator);
        }

        public static void ResetCache(string startsWith = null) {
            if (string.IsNullOrEmpty(startsWith)) {
                _packageCache.Clear();
                return;
            }

            var invalidKeys = _packageCache.Keys.Where(x => x.StartsWith(startsWith, StringComparison.InvariantCultureIgnoreCase));
            foreach (var key in invalidKeys) {
                _packageCache.Remove(key);
            }
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args) {
            //var assemblyPath = args.RequestingAssembly.Location;
            var packageName = new AssemblyName(args.Name).Name;
            if (_packageCache.ContainsKey(packageName)) {
                return _packageCache[packageName];
            }

            var package = _packageLocators.Select(x => x.Resolve(packageName)).FirstOrDefault(x => x != null);
            var assembly = package?.MainAssembly;

            _packageCache.Add(packageName, assembly);

            return assembly;
        }
    }
}

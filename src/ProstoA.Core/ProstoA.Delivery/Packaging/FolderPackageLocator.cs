using System.IO;

namespace ProstoA.Delivery.Packaging {
    public class FolderPackageLocator : IPackageLocator {
        private readonly string _path;

        public FolderPackageLocator(string path) {
            _path = path;
        }

        public IPackage Resolve(string name) {
            var location = Path.Combine(_path, name + ".dll");
            return new AssemblyPackage(location, name);
        }
    }
}
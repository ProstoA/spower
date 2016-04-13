using System.IO;
using System.Reflection;

namespace ProstoA.Delivery.Packaging {
    public class AssemblyPackage : IPackage {
        private readonly string _location;

        public AssemblyPackage(string location, string name, string title = null) {
            _location = location;
            Name = name;
            Title = title ?? name;
        }

        public string Name { get; }

        public string Title { get; set; }

        public Assembly MainAssembly => File.Exists(_location) ? Assembly.LoadFile(_location) : null;
    }
}
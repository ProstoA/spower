using System.Reflection;

namespace ProstoA.Delivery.Packaging {
    public interface IPackage {
        string Name { get; }

        string Title { get; set; }

        Assembly MainAssembly { get; }
    }
}
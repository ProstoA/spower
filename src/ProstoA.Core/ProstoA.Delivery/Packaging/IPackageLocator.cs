namespace ProstoA.Delivery.Packaging {
    public interface IPackageLocator {
        IPackage Resolve(string name);
    }
}
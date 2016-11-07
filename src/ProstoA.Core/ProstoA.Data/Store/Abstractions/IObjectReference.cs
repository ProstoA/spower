using ProstoA.Data.Metamodel;

namespace ProstoA.Data.Store {
    public interface IObjectReference<T> {
        IObjectIdentity Identity { get; }

        IObjectContainer<T> Container { get; }
    }
}
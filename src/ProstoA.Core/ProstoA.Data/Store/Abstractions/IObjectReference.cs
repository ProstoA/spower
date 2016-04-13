using ProstoA.Data.Model.Abstractions;

namespace ProstoA.Data.Store.Abstractions {
    public interface IObjectReference<T> {
        IObjectIdentity Identity { get; }

        IObjectContainer<T> Container { get; }
    }
}
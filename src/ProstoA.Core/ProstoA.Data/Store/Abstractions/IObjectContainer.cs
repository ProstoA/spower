using ProstoA.Data.Object.Abstractions;

namespace ProstoA.Data.Store.Abstractions {
    //public interface IObjectContainer<T> : IHierarchical<IObjectIdentity> {
    //    T Resolve(Object.Abstractions.IObjectIdentity<T> identity);
    //}

    public interface IObjectContainer<T> {
        T GetObject(IObjectIdentity<T> identity);

        void AddOrUpdate(IObjectIdentity<T> identity, T item);

        void Remove(IObjectIdentity<T> identity);

        void SaveChanges();
    }
}
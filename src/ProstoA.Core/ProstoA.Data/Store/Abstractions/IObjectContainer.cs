using ProstoA.Data.Metamodel;

namespace ProstoA.Data.Store {
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
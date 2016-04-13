//using ProstoA.Data.Store.Abstractions;

using ProstoA.Data.Object.Abstractions;

namespace ProstoA.Data.Store.Abstractions {
    public interface IEntity<T> : IObjectIdentity<T>, IObjectReference<T> { }

    //public class Book : IEntity<Book> {
    //    IObjectClass IObjectIdentity.Class => null;

    //    string IObjectIdentity.Name => "Book#{n}";

    //    SemanticVersion IObjectIdentity.Version => null;

    //    IObjectIdentity IObjectReference<Book>.Identity => this;

    //    IObjectContainer<Book> IObjectReference<Book>.Container => null;
    //}
}
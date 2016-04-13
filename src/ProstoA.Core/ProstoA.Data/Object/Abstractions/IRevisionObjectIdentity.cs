namespace ProstoA.Data.Object.Abstractions {
    public interface IRevisionObjectIdentity<T> : IObjectIdentity<T> {
        IObjectIdentity<IObjectRevision<T>> Revision { get; }
    }
}
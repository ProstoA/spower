namespace ProstoA.Data.Metamodel {
    public interface IRevisionObjectIdentity<T> : IObjectIdentity<T> {
        IObjectIdentity<IObjectRevision<T>> Revision { get; }
    }
}
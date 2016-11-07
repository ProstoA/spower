using ProstoA.Data.Metamodel;

namespace ProstoA.Operations.Queries {
    public interface IHistoryQuery<T> : ISubsetQuery<IObjectRevision<T>> { }
}
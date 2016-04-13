using ProstoA.Data.Object.Abstractions;

namespace ProstoA.Operations.Queries {
    public interface IHistoryQuery<T> : ISubsetQuery<IObjectRevision<T>> { }
}
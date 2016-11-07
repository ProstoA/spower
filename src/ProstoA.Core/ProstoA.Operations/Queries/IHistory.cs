using ProstoA.Data.Metamodel;

namespace ProstoA.Operations.Queries {
    public interface IHistory<T> {
        IQueryHandler<IHistoryQuery<T>, ISubset<IObjectRevision<T>>> History { get; }
    }
}
using ProstoA.Data.Object.Abstractions;

namespace ProstoA.Operations.Queries {
    public interface IHistory<T> {
        IQueryHandler<IHistoryQuery<T>, ISubset<IObjectRevision<T>>> History { get; }
    }
}
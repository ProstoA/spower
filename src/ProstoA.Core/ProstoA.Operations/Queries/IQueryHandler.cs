using System.Threading.Tasks;

namespace ProstoA.Operations.Queries {
    public interface IQueryHandler<in TQuery, out TResult> where TQuery : IQuery<TResult> {
        TResult Execute(TQuery query);
    }

    public interface IAsyncQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult> {
        Task<TResult> Execute(TQuery query);
    }
}
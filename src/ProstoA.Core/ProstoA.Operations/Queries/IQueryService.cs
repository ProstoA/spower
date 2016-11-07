using System.Threading.Tasks;

namespace ProstoA.Operations.Queries {
    public interface IQueryService {
        TResult Execute<TResult>(IQuery<TResult> query);
    }

    public interface IAsyncQueryService {
        Task<TResult> ExecuteAsync<TResult>(IQuery<TResult> query);
    }
}

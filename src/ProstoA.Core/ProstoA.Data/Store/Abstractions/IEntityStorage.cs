using System.Threading.Tasks;

using ProstoA.Data.Object.Abstractions;

namespace ProstoA.Data.Store.Abstractions {
    public interface IEntityStorage {
        IDataCollection<TEntity> Collection<TEntity>() where TEntity : class;

        //Task<TResult> ExecuteQuery<TResult>(IDataQuery<TResult> query);

        Task AddOrUpdate(params IDataObject[] items);

        Task Remove(params IDataObject[] items);
    }
}
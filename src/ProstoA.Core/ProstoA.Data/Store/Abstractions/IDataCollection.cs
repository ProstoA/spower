using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProstoA.Data.Store.Abstractions {
    public interface IDataCollection<TEntity> : IObjectContainer<TEntity> {
        TEntity GetByKey(object key);

        Task<TEntity> GetByKeyAsync(object key);

        TEntity[] GetBySpec(Expression<Func<TEntity,bool>> predicate);

        Task<TEntity[]> GetBySpecAsync(Expression<Func<TEntity,bool>> predicate);

        TEntity GetFirstBySpec(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> GetFirstBySpecAsync(Expression<Func<TEntity, bool>> predicate);

        void AddOrUpdate(TEntity entity);

        Task AddOrUpdateAsync(TEntity entity);

        void AddOrUpdate(TEntity entity, Expression<Func<TEntity, bool>> predicate, params string[] ignore);

        Task AddOrUpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate, params string[] ignore);

        void AddOrUpdateRange(IEnumerable<TEntity> entities);

        Task AddOrUpdateRangeAsync(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        Task RemoveAsync(TEntity entity);
    }
}
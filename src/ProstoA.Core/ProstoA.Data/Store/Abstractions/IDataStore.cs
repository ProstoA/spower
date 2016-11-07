using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using ProstoA.Data.Model;
using ProstoA.Data.Store.Mappers;

namespace ProstoA.Data.Store {
    public interface IDataStore<T> : IDisposable where T : IRootEntity<T> {
        T Get(IReference<T> reference, IMappingOptions options);

        void Put(T entity, IMappingOptions options);
    }

    public interface IAsyncDataStore<T> : IDisposable where T : IRootEntity<T> {
        Task<T> GetAsync(IReference<T> reference, IMappingOptions options);

        Task PutAsync(T entity, IMappingOptions options);
    }

    public interface ISubset<out T> : IEnumerable<T> {
        int Offset { get; }

        int TotalCount { get; }
    }

    public interface IQuerySpecification<T, TResult> {
        ISelectingSpecification<T, TResult> Select { get; }

        IFilteringSpecification<T> Filter { get; }

        IOrderingSpecification<T> Order { get; }

        IPaggingSpecification Page { get; }
    }

    public interface ISelectingSpecification<T, TResult> {
        Expression<Func<T, TResult>> Expression { get; }
    }

    public interface IFilteringSpecification<T> {
        Expression<Func<T, bool>> Expression { get; }
    }

    public interface IOrderingSpecification<T> {
        Expression<Func<IQueryable<T>, IOrderedQueryable<T>>> Expression { get; }
    }

    public interface IPaggingSpecification {
        int Offset { get; }

        int Limit { get; }
    }

    public interface IQueriedDataStore<T> : IDisposable where T : IRootEntity<T> {
        ISubset<TResult> GetBySpec<TResult>(IQuerySpecification<T, TResult> specification, IMappingOptions options);
    }

    public interface IAsyncQueriedDataStore<TEntity> : IDisposable where TEntity : IRootEntity<TEntity> {
        Task<ISubset<TResult>> GetBySpecAsync<TResult>(IQuerySpecification<TEntity, TResult> specification, IMappingOptions options);
    }
}
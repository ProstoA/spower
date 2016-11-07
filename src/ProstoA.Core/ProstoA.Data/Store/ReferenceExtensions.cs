using System;
using System.Threading.Tasks;

namespace ProstoA.Data.Store {
    public static class ReferenceExtensions {
        public static bool HasObject<TKey, TEntity>(this IReference<TKey, TEntity> reference) where TEntity : class {
            return reference is TEntity;
        }

        public static TEntity GetObject<TKey, TEntity>(this IReference<TKey, TEntity> reference, IEntityStorage store = null) where TEntity : class {
            if (reference == null) {
                return null;
            }

            var result = reference as TEntity;

            if (result != null) {
                return result;
            }

            if (store == null) {
                throw new InvalidOperationException("Requires recourse to data storage");
            }

            return store.Collection<TEntity>().GetByKey(reference.GetKey());
        }

        public static Task<TEntity> GetObjectAsync<TKey, TEntity>(this IReference<TKey, TEntity> reference, IEntityStorage store = null) where TEntity : class {
            if (reference == null) {
                return null;
            }

            var result = reference as TEntity;

            if (result != null) {
                return Task.FromResult(result);
            }

            if (store == null){
                throw new InvalidOperationException("Requires recourse to data storage");
            }

            return store.Collection<TEntity>().GetByKeyAsync(reference.GetKey());
        }
    }
}
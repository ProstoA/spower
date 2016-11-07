namespace ProstoA.Data.Model {
    public class DomainEntityReference<TEntity, TKey> : IReference<TEntity>
        where TKey : IKey<TEntity> {

        internal DomainEntityReference(IKey<TEntity> key) {
            Key = key;
        }

        public IKey<TEntity> Key { get; }

        public static implicit operator DomainEntityReference<TEntity, TKey>(TKey key) {
            return new DomainEntityReference<TEntity, TKey>(key);
        }
    }
}
using System;

namespace ProstoA.Data.Model {
    public abstract class DomainEntity<TEntity, TKey> : IEntity<TEntity>
        where TEntity : IEntity<TEntity>
        where TKey : IKey<TEntity> {

        public abstract TKey Key { get; }

        IKey<TEntity> IReference<TEntity>.Key => Key;

        public bool Equals(IEntity<TEntity> other) {
            if (ReferenceEquals(this, other)) {
                return true;
            }

            if (ReferenceEquals(null, other)) {
                return false;
            }

            if (ReferenceEquals(Key, other.Key)) {
                return true;
            }

            if (ReferenceEquals(null, other.Key)) {
                return false;
            }

            return other.Key.Equals(Key);
        }

        #region Equals

        public override bool Equals(object obj) {
            return Equals(obj as IEntity<TEntity>);
        }

        public override int GetHashCode() {
            return Key?.GetHashCode() ?? 0;
        }

        bool IEquatable<IEntity<TEntity>>.Equals(IEntity<TEntity> other) {
            throw new NotImplementedException();
        }

        public static bool operator ==(DomainEntity<TEntity, TKey> left, IEntity<TEntity> right) {
            return Equals(left, right);
        }

        public static bool operator !=(DomainEntity<TEntity, TKey> left, IEntity<TEntity> right) {
            return !Equals(left, right);
        }

        #endregion

        public sealed class Reference : IReference<TEntity> {
            internal Reference(IKey<TEntity> key) {
                Key = key;
            }

            public IKey<TEntity> Key { get; }

            public static implicit operator Reference(TKey key) {
                return new Reference(key);
            }
        }
    }
}
using System;
using System.Collections.Generic;

using ProstoA.Data.Store.Abstractions;


namespace ProstoA.Data.Store {
    [Serializable]
    public class EntityReference<TKey, TEntity> : IReference<TKey, TEntity>, IEquatable<IReference<TKey, TEntity>> {
        public static EntityReference<TKey, TEntity> Empty = new EntityReference<TKey, TEntity>(); 

        public TKey Id { get; set; }

        TKey IReference<TKey, TEntity>.GetKey() {
            return Id;
        }

        public static implicit operator EntityReference<TKey, TEntity>(TKey key) {
            return Equals(key, default(TKey)) 
                ? Empty 
                : new EntityReference<TKey, TEntity> { Id = key };
        }

        #region Equality members

        public bool Equals(IReference<TKey, TEntity> other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return EqualityComparer<TKey>.Default.Equals(Id, other.GetKey());
        }

        public override bool Equals(object obj) {
            return Equals(obj as IReference<TKey, TEntity>);
        }

        public override int GetHashCode() {
            return EqualityComparer<TKey>.Default.GetHashCode(Id);
        }

        public static bool operator ==(EntityReference<TKey, TEntity> left, EntityReference<TKey, TEntity> right) {
            return Equals(left, right);
        }

        public static bool operator !=(EntityReference<TKey, TEntity> left, EntityReference<TKey, TEntity> right) {
            return !Equals(left, right);
        }

        #endregion
    }
}
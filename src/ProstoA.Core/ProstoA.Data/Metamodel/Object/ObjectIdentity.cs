using System;
using System.Collections.Generic;

namespace ProstoA.Data.Metamodel {
    public class ObjectClass : IObjectClass {
        private readonly Type _type;

        public ObjectClass(Type type) {
            _type = type;
        }

        public string Name => _type.AssemblyQualifiedName;

        public IEnumerable<IObjectClass> Parents {
            get {
                var parent = _type.BaseType;
                while (parent != null) {
                    yield return new ObjectClass(parent);
                    parent = parent.BaseType;
                }
            }
        }
    }

    public class ObjectIdentity<T> : IObjectIdentity<T> {
        public ObjectIdentity(string key) {
            Key = key;
        }

        public IObjectClass Class => new ObjectClass(typeof(T));

        public string Key { get; }

        protected bool Equals(ObjectIdentity<T> other) {
            return string.Equals(Key, other.Key);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            if (obj.GetType() != this.GetType()) {
                return false;
            }
            return Equals((ObjectIdentity<T>) obj);
        }

        public override int GetHashCode() {
            return Key?.GetHashCode() ?? 0;
        }
    }

    public class RevisionObjectIdentity<T> : IRevisionObjectIdentity<T> {
        public RevisionObjectIdentity(string key, string revision) {
            Key = key;
            Revision = new ObjectIdentity<IObjectRevision<T>>(revision);
        }

        public IObjectClass Class => new ObjectClass(typeof(T));

        public string Key { get; }

        public IObjectIdentity<IObjectRevision<T>> Revision { get; }

        protected bool Equals(RevisionObjectIdentity<T> other) {
            return string.Equals(Key, other.Key) && Equals(Revision, other.Revision);
        }

        public override bool Equals(object obj) {
            if(ReferenceEquals(null, obj)) {
                return false;
            }
            if(ReferenceEquals(this, obj)) {
                return true;
            }
            if(obj.GetType() != this.GetType()) {
                return false;
            }
            return Equals((RevisionObjectIdentity<T>)obj);
        }

        public override int GetHashCode() {
            unchecked {
                return ((Key?.GetHashCode() ?? 0) * 397) ^ (Revision?.GetHashCode() ?? 0);
            }
        }
    }
}
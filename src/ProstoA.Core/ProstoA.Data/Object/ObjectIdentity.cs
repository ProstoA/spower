using ProstoA.Data.Object.Abstractions;

namespace ProstoA.Data.Object {
    public class ObjectIdentity<T> : IObjectIdentity<T> {
        public ObjectIdentity(string key) {
            Key = key;
        }

        public string Type => typeof(T).AssemblyQualifiedName;

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

        public string Type => typeof (T).AssemblyQualifiedName;

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
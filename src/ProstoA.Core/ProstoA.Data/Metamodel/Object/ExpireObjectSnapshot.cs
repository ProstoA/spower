using System;
using System.Collections.Generic;
using System.Linq;

namespace ProstoA.Data.Metamodel {
    public class ExpireObjectSnapshot<T> : Dictionary<IObjectIdentity<IDataItem<T>>, object>, IObjectSnapshot<T>, IExpire {
        private readonly IRevisionObjectIdentity<T> _identity;

        public ExpireObjectSnapshot(IRevisionObjectIdentity<T> identity, Period validity) {
            _identity = identity;
            Validity = validity;
        }

        public ExpireObjectSnapshot(IRevisionObjectIdentity<T> identity, Period validity, IDictionary<IObjectIdentity<IDataItem<T>>, object> data) : base(data) {
            _identity = identity;
            Validity = validity;
        }

        public Period Validity { get; private set; }

        public bool ValidOn(DateTimeOffset date) {
            return Validity.Contains(date);
        }

        public void Expire(DateTimeOffset date) {
            Validity = new Period(Validity.Start, date);
        }

        public ExpireObjectSnapshot<T> ApplyChanges(IObjectIdentity<IObjectRevision<T>> revision, Period validity, params IObjectDiff<T>[] diffs) {
            var copy = new ExpireObjectSnapshot<T>(new RevisionObjectIdentity<T>(_identity.Key, revision.Key), validity, this);
            ApplyChanges(copy, diffs);
            return copy;
        }

        IObjectSnapshot<T> IObjectSnapshot<T>.ApplyChanges(IObjectIdentity<IObjectRevision<T>> revision, params IObjectDiff<T>[] diffs) {
            return ApplyChanges(revision, Validity, diffs);
        }

        public static ExpireObjectSnapshot<T> Create(IRevisionObjectIdentity<T> identity, Period validity, params IObjectDiff<T>[] diffs) {
            var snapshot = new ExpireObjectSnapshot<T>(identity, validity);
            ApplyChanges(snapshot, diffs);
            return snapshot;
        }

        IObjectClass IObjectIdentity.Class => _identity.Class;

        string IObjectIdentity.Key => _identity.Key;

        public IObjectIdentity<IObjectRevision<T>> Revision => _identity.Revision;

        private static void ApplyChanges(IObjectSnapshot<T> snapshot, params IObjectDiff<T>[] diffs) {
            foreach(var diff in diffs.SelectMany(x => x)) {
                switch(diff.Value.Status) {
                    case ChangeStatus.Added:
                        snapshot.Add(diff.Key, diff.Value.NewValue);
                        break;

                    case ChangeStatus.Modified:
                        snapshot[diff.Key] = diff.Value.NewValue;
                        break;

                    case ChangeStatus.Removed:
                        snapshot.Remove(diff.Key);
                        break;

                    case ChangeStatus.NoChanges:
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
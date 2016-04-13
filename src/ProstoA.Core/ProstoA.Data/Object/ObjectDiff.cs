using System.Collections.Generic;

using ProstoA.Data.Object.Abstractions;

namespace ProstoA.Data.Object {
    public class ObjectDiff<T> : Dictionary<IObjectIdentity<Model.Abstractions.IDataItem<T>>, IValueChange>, IObjectDiff<T> {
        public ObjectDiff(ChangeStatus status) {
            Status = status;
        }

        public ObjectDiff(ChangeStatus status, IDictionary<IObjectIdentity<Model.Abstractions.IDataItem<T>>, IValueChange> dictionary) : base(dictionary) {
            Status = status;
        }

        public ChangeStatus Status { get; }
    }
}
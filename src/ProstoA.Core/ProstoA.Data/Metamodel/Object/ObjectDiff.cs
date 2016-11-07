using System.Collections.Generic;

namespace ProstoA.Data.Metamodel {
    public class ObjectDiff<T> : Dictionary<IObjectIdentity<IDataItem<T>>, IValueChange>, IObjectDiff<T> {
        public ObjectDiff(ChangeStatus status) {
            Status = status;
        }

        public ObjectDiff(ChangeStatus status, IDictionary<IObjectIdentity<IDataItem<T>>, IValueChange> dictionary) : base(dictionary) {
            Status = status;
        }

        public ChangeStatus Status { get; }
    }
}
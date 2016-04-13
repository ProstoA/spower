using System.Collections.Generic;

using ProstoA.Data.Model.Abstractions;

namespace ProstoA.Data.Model {
    public class DataField<T> : ISimpleDataModel, IDataModelFix, IObjectDisplay {
        private readonly string _name;

        public DataField(string name, string title = null, DataConstraints constraints = null) {
            _name = name;
            Title = title ?? name;
            Constraints = constraints;
        }

        public IEnumerable<IObjectIdentity> Parents { get; }

        public IObjectIdentity Identity => new SimpleObjectIdentity(_name);

        public IObjectDisplay Display => this;

        public string Title { get; }

        public string Description { get; }

        public string DataType { get; }

        public bool Readonly { get; }

        public string Default { get; }

        public string Prompt { get; }

        public DataConstraints Constraints { get; }

        IEnumerable<IDataModel> IDataModelFix.Applay(IEnumerable<IDataModel> items) {
            return items.Append(this);
        }
    }
}
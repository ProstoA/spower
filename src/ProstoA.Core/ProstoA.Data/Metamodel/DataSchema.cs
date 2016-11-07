using System;
using System.Collections.Generic;
using System.Linq;

namespace ProstoA.Data.Metamodel {
    public class DataSchema : IComplexDataModel, IObjectDisplay {
        private readonly string _name;
        private readonly IEnumerable<IDataModel> _items;

        public DataSchema(string name, string title, params IDataModel[] items)
            : this(name, title, (IEnumerable<IDataModel>) items.OfType<IDataModel>()) {
        }

        public DataSchema(string name, string title, IEnumerable<IDataModel> items) {
            _name = name;
            _items = items;
            Title = title ?? name;
        }

        public IDataModel this[string name] {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<IObjectIdentity> Parents { get; }

        public IObjectIdentity Identity => new SimpleObjectIdentity(_name);

        public IObjectDisplay Display => this;

        IEnumerable<string> IComplexDataModel.Items => _items.Select(x => x.Identity.Key);

        public string Title { get; }

        public string Description { get; }
    }
}
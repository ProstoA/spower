using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ProstoA.Data.Metamodel {
    [Serializable]
    public class DataModel : IComplexDataModel, IObjectDisplay {
        private readonly Dictionary<string, PropertyDataItemModel> _items;

        public DataModel(Type type) {
            Parents = Enumerable.Empty<IObjectIdentity>();
            Name = type.Name;
            Title = type.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? Name;
            Description = type.GetCustomAttribute<DescriptionAttribute>()?.Description;

            var props = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
            _items = props.Select(x => new PropertyDataItemModel(this, x)).ToDictionary(x => x.Name);
        }

        public IEnumerable<IObjectIdentity> Parents { get; }

        public string Name { get; }

        public string Title { get; }

        public string Description { get; }

        public IEnumerable<string> Items => _items.Keys;

        public IDataModel this[string name] => _items[name];

        IObjectIdentity IDataModel.Identity => new SimpleObjectIdentity(Name);

        IObjectDisplay IDataModel.Display => this;
    }
}
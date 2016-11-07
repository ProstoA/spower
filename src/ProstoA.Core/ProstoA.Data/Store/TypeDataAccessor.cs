using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using ProstoA.Data.Metamodel;

namespace ProstoA.Data.Store {
    public class TypeDataAccessor : IDataAccessor, IDataMutator {
        private readonly object _data;
        private readonly IDictionary<string, PropertyInfo> _items;

        public TypeDataAccessor(Type type) : this(Activator.CreateInstance(type)) { }

        public TypeDataAccessor(object data) {
            _data = data;
            _items = data.GetType()
                .GetProperties(BindingFlags.DeclaredOnly|BindingFlags.Public|BindingFlags.Instance)
                .ToDictionary(x => x.Name);
        }

        // todo: сюда нужно вставить конвертеры значений
        public string GetValue(string name) {
            return Convert.ToString(_items[name].GetValue(_data));
        }

        // todo: сюда нужно вставить конвертеры значений
        public void SetValue(string name, string value) {
            _items[name].SetValue(_data, value);
        }

        public Dictionary<string, string> ToDictionary() {
            return _items.ToDictionary(x => x.Key, x => GetValue(x.Key));
        }
    }
}
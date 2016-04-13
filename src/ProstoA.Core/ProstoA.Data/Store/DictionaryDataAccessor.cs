using System.Collections.Generic;
using System.Linq;

using ProstoA.Data.Object.Abstractions;

namespace ProstoA.Data.Store {
    public class DictionaryDataAccessor : IDataAccessor {
        private readonly IDictionary<string, string> _items;

        public DictionaryDataAccessor(IDictionary<string,string> items) {
            _items = items;
        }

        // todo: сюда нужно вставить конвертеры значений
        public string GetValue(string name) {
            return _items[name];
        }

        // todo: сюда нужно вставить конвертеры значений
        public void SetValue(string name, string value) {
            _items[name] = value;
        }

        public Dictionary<string, string> ToDictionary() {
            return _items.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
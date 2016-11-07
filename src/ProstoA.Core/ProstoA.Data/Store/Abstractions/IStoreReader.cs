using System.Collections.Generic;
using System.Collections.Specialized;

namespace ProstoA.Data.Store {
    public interface IStoreReader<in TStore> {
        object GetValue(TStore store, string name);
    }

    public class NameValueCollectionReader : IStoreReader<NameValueCollection> {
        private readonly IDictionary<string, string> _fieldMap;

        public NameValueCollectionReader(IDictionary<string, string> fieldMap) {
            _fieldMap = fieldMap;
        }

        public object GetValue(NameValueCollection store, string name) {
            var key = _fieldMap.ContainsKey(name) ? _fieldMap[name] : name;

            return store[key];
        }
    }
}
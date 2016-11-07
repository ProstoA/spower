using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProstoA.Data.Store.Mappers {
    public class MappingOptions : IMappingOptions {
        private static readonly Func<Type, string> GetKey = x => x.FullName;

        private readonly IDictionary<string, object> _data;

        public MappingOptions(params object[] options) {
            _data = Prepare(options);
        }

        public object Get(Type type, object defaultValue = null) {
            var key = GetKey(type);
            return _data.ContainsKey(key) ? _data[key] : defaultValue;
        }

        public IMappingOptions Extend(IMappingOptions options) {
            return new MappingOptions(this, options);
        }

        private static IDictionary<string, object> Prepare(params object[] options) {
            return options
                .SelectMany(item => item as IMappingOptions ?? (IEnumerable<object>)new[] { item })
                .GroupBy(x => GetKey(x.GetType()))
                .ToDictionary(x => x.Key, x => x.Last());
        }

        public IEnumerator<object> GetEnumerator() {
            return _data.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
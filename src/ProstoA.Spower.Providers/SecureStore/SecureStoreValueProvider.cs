using System;
using System.Globalization;

using ProstoA.Data;
using ProstoA.Data.ModelBinding;

namespace ProstoA.Spower.Providers.SecureStore {
    public class SecureStoreValueProvider : IValueProvider {
        private readonly SecureStoreContainer _container;

        public SecureStoreValueProvider(SecureStoreContainer container) {
            _container = container;
        }

        public bool ContainsPrefix(string prefix) {
            throw new NotImplementedException();
        }

        public IValueProviderResult GetValue(string key) {
            var value = _container.ReadField(key);
            return new ValueProviderResult(value, value.ToString(), CultureInfo.InvariantCulture);
        }
    }
}
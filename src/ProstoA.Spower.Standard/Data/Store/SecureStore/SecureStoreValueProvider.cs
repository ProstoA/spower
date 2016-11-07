using System;
using System.Globalization;

using ProstoA.Data.Store;
using ProstoA.Data.Store.ModelBinding;

namespace ProstoA.Spower.Standard.Data.Store.SecureStore {
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
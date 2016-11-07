using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;

using Microsoft.Office.SecureStoreService.Server;

using ProstoA.Data.Store;

namespace ProstoA.Spower.Standard.Data.Store.SecureStore {
    public class SecureStoreContainer : IDataContainer {
        private readonly TargetApplication _app;
        private readonly SecureStoreProvider _provider;

        public SecureStoreContainer(TargetApplication app, SecureStoreProvider provider) {
            _app = app;
            _provider = provider;
        }

        public string Key => _app.ApplicationId;

        public string Title => _app.FriendlyName;

        public IEnumerable<string> Column => _provider.GetTargetApplicationFields(_app.ApplicationId)
            .OfType<TargetApplicationField>()
            .Select(x => x.Name);

        public IDictionary<string, object> Read(object key, params string[] columns) {
            var providerFields = _provider.GetTargetApplicationFields(_app.ApplicationId)
                .OfType<TargetApplicationField>();

            using(var data = _provider.GetCredentials(_app.ApplicationId)) {
                var values = providerFields.Select((x, i) => new { Field = x, Value = data[i].Credential })
                    .ToLookup(x => x.Field.Name, x => x.Field.IsMasked ? (object)x.Value : ConvertToString(x.Value));

                return columns.Length == 0
                    ? values.ToDictionary(x => x.Key, x => x.FirstOrDefault())
                    : columns.ToDictionary(x => x, x => values[x].FirstOrDefault());
            }
        }

        public static string ConvertToString(SecureString secureString) {
            var ptr = Marshal.SecureStringToBSTR(secureString);
            try {
                return Marshal.PtrToStringBSTR(ptr);
            } finally {
                Marshal.FreeBSTR(ptr);
            }
        }
    }
}
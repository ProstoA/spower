using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Office.SecureStoreService.Server;
using Microsoft.SharePoint;

using ProstoA.Data.Store;

namespace ProstoA.Spower.Standard.Data.Store.SecureStore {
    public class SecureStoreDataStorage : IDataStorage {
        private readonly SecureStoreProvider _provider;

        public SecureStoreDataStorage(SecureStoreProvider provider) {
            _provider = provider;
        }

        public static SecureStoreDataStorage ForSite(SPSite site) {
            return new SecureStoreDataStorage(new SecureStoreProvider { Context = SPServiceContext.GetContext(site) });
        }

        public IEnumerable<IDataContainer> Containers => _provider.GetTargetApplications()
            .OfType<TargetApplication>()
            .Select(x => new SecureStoreContainer(x, _provider));

        public bool ContainsContainer(string key) {
            if(string.IsNullOrEmpty(key)) {
                throw new ArgumentException("Container name can not be null.", nameof(key));
            }

            return _provider.GetTargetApplications()
                .OfType<TargetApplication>()
                .Any(x => x.ApplicationId == key);
        }

        public IDataContainer GetContainer(string key) {
            var app = (TargetApplication)_provider.GetTargetApplication(key);
            return new SecureStoreContainer(app, _provider);
        }
    }
}
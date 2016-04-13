using System.Collections.Generic;

namespace ProstoA.Data.Store.Abstractions {
    public interface IDataStorage {
        IEnumerable<IDataContainer> Containers { get; }

        bool ContainsContainer(string key);

        IDataContainer GetContainer(string key);
    }
}
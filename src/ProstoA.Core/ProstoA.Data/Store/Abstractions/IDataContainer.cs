using System.Collections.Generic;

namespace ProstoA.Data.Store.Abstractions {
    public interface IDataContainer {
        string Key { get; }

        string Title { get; }

        IEnumerable<string> Column { get; }

        IDictionary<string, object> Read(object key, params string[] columns);
    }
}
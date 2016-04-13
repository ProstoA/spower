using System.Collections.Generic;

namespace ProstoA.Data.Object.Abstractions {
    public interface IDataAccessor {
        string GetValue(string name);

        Dictionary<string, string> ToDictionary();
    }

    public interface IDataMutator {
        void SetValue(string name, string value);
    }
}
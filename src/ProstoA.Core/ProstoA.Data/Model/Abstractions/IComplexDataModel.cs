using System.Collections.Generic;

namespace ProstoA.Data.Model.Abstractions {
    public interface IComplexDataModel : IDataModel {
        IEnumerable<string> Items { get; }

        IDataModel this[string name] { get; } // GetItemByPath

        //IEnumerable<IDataModel> Items { get; set; }
    }
}
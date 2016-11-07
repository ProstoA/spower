using System.Collections.Generic;

namespace ProstoA.Data.Metamodel {
    public interface IComplexDataModel : IDataModel {
        IEnumerable<string> Items { get; }

        IDataModel this[string name] { get; } // GetItemByPath

        //IEnumerable<IDataModel> Items { get; set; }
    }
}
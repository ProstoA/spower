using System.Collections.Generic;

namespace ProstoA.Data.Metamodel {
    public interface IDataModelFix {
        IEnumerable<IDataModel> Applay(IEnumerable<IDataModel> items);
    }
}
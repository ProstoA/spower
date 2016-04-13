using System.Collections.Generic;

namespace ProstoA.Data.Model.Abstractions {
    public interface IDataModelFix {
        IEnumerable<IDataModel> Applay(IEnumerable<IDataModel> items);
    }
}
using ProstoA.Data.Model.Abstractions;

namespace ProstoA.Data.Object.Abstractions {
    public interface IDataObject {
        //IObjectIdentity Identity { get; }

        IDataModel Model { get; }
    }
}
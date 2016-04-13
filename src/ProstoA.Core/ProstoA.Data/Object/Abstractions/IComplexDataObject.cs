using ProstoA.Data.Model.Abstractions;

namespace ProstoA.Data.Object.Abstractions {
    public interface IComplexDataObject : IDataObject {
        new IComplexDataModel Model { get; }

        IDataObject this[string name] { get; set; }
    }
}
using ProstoA.Data.Model.Abstractions;

namespace ProstoA.Data.Object.Abstractions {
    public interface ISimpleDataObject : IDataObject {
        new ISimpleDataModel Model { get; }

        string Value { get; }
    }
}
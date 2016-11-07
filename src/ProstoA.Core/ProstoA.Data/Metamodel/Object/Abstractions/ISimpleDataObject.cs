namespace ProstoA.Data.Metamodel {
    public interface ISimpleDataObject : IDataObject {
        new ISimpleDataModel Model { get; }

        string Value { get; }
    }
}
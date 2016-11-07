namespace ProstoA.Data.Metamodel {
    public interface IComplexDataObject : IDataObject {
        new IComplexDataModel Model { get; }

        IDataObject this[string name] { get; set; }
    }
}
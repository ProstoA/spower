namespace ProstoA.Data.Metamodel {
    public interface ISimpleDataModel : IDataModel {
        string DataType { get; }

        bool Readonly { get; }

        string Default { get; }

        string Prompt { get; }
    }
}
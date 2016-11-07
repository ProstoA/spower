using ProstoA.Data.Model;

namespace ProstoA.Data.Metamodel {
    public interface IObjectIdentity<T> : IObjectIdentity { }

    public interface IObjectIdentity {
        IObjectClass Class { get; } // string type

        string Key { get; }

        //string Name { get; } // string Key

        //SemanticVersion Version { get; }
    }

    public interface IObjectClass : IHierarchical<IObjectClass> {
        string Name { get; }
    }
}
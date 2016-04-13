namespace ProstoA.Data.Model.Abstractions {
    public interface IObjectIdentity<T> : IObjectIdentity { }

    public interface IObjectIdentity {
        IObjectClass Class { get; }

        string Name { get; }

        SemanticVersion Version { get; }
    }

    public interface IObjectClass : IHierarchical<IObjectClass> {
        string Name { get; }
    }
}
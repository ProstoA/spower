namespace ProstoA.Data.Object.Abstractions {
    public interface IObjectIdentity<T> {
        string Type { get; }

        string Key { get; }
    }
}
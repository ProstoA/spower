namespace ProstoA.Data.Model {
    /// <summary>
    /// Корень агрегата
    /// </summary>
    public interface IRootEntity<T> : IEntity<T> where T : IRootEntity<T> { }
}
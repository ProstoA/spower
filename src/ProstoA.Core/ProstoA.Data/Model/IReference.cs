namespace ProstoA.Data.Model {
    /// <summary>
    /// Абстракция ссылки на объект
    /// </summary>
    /// <typeparam name="T">Тип оъекта</typeparam>
    public interface IReference<T> {
        IKey<T> Key { get; }
    }
}
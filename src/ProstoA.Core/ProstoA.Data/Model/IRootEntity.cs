namespace ProstoA.Data.Model {
    /// <summary>
    /// ������ ��������
    /// </summary>
    public interface IRootEntity<T> : IEntity<T> where T : IRootEntity<T> { }
}
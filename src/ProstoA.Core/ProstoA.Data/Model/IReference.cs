namespace ProstoA.Data.Model {
    /// <summary>
    /// ���������� ������ �� ������
    /// </summary>
    /// <typeparam name="T">��� ������</typeparam>
    public interface IReference<T> {
        IKey<T> Key { get; }
    }
}
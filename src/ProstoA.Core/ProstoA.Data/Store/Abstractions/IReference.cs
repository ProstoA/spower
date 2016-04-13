namespace ProstoA.Data.Store.Abstractions {
    public interface IReference<out TKey, TEntity> {
        TKey GetKey();
    }
}
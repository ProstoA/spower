namespace ProstoA.Data.Store {
    public interface IReference<out TKey, TEntity> {
        TKey GetKey();
    }
}
using ProstoA.Data.Metamodel;

namespace ProstoA.Data.Store {
    public interface IStoreWriter<in TStore> {
        void GetValue(TStore store, IDataModel model, object value);
    }
}
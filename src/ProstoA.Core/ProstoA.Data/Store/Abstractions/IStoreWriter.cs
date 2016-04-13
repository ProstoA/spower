using ProstoA.Data.Model.Abstractions;

namespace ProstoA.Data.Store.Abstractions {
    public interface IStoreWriter<in TStore> {
        void GetValue(TStore store, IDataModel model, object value);
    }
}
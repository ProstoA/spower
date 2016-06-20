using ProstoA.Data.Model.Abstractions;

namespace ProstoA.Data.Presentation {
    public interface ITemplate<in TData, in TForm> where TForm : IForm<TData> {
        IView Apply(TData data, TForm form);
    }
}
namespace ProstoA.Data.View {
    public interface ITemplate<in TModel, out TResult> {
        TResult Apply(TModel model);
    }
}
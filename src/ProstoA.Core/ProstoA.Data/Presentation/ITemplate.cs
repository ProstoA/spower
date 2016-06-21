namespace ProstoA.Data.Presentation {
    public interface ITemplate<in TModel, out TResult> {
        TResult Apply(TModel model);
    }
}
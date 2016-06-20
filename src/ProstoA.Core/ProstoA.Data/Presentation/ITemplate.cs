namespace ProstoA.Data.Presentation {
    public interface ITemplate<in TData> {
        IView Apply(TData data);
    }
}
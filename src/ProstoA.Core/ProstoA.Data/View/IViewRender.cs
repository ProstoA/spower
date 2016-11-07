namespace ProstoA.Data.View {
    public interface IViewRender<in TView, in TContext> where TView : IView {
        void Render(TView view, TContext context);
    }
}
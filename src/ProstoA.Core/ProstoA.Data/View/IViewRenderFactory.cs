namespace ProstoA.Data.View {
    public interface IViewRenderFactory {
        void Render<TContext>(IView view, TContext context);
    }
}
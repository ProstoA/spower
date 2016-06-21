namespace ProstoA.Data.Presentation {
    public interface IViewRenderFactory {
        void Render<TContext>(IView view, TContext context);
    }
}
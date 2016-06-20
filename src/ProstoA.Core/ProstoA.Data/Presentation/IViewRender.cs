namespace ProstoA.Data.Presentation {
    public interface IViewRender<in TView> {
        void Render(TView view);
    }
}
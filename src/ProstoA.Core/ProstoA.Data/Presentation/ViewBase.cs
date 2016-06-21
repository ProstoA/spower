using System.Threading;

namespace ProstoA.Data.Presentation {
    public abstract class ViewBase<TState> : IView {
        private TState _state;
        private bool _initialized;
        private object _sync = new object();

        protected TState State {
            get {
                Compile();
                return _state;
            }
        }

        protected abstract TState CreateState();

        public void Compile() {
            LazyInitializer.EnsureInitialized(ref _state, ref _initialized, ref _sync, CreateState);
        }
    }
}
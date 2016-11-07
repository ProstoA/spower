namespace ProstoA.Operations.Events {
    public interface IEventHandler<in T> {
        // bool CanExecute(T @event);

        void Execute(T @event);
    }
}
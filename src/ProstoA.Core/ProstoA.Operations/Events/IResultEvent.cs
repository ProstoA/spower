namespace ProstoA.Operations.Events {
    public interface IResultEvent<out T> : IEvent {
        T Data { get; }
    }
}
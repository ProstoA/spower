namespace ProstoA.Operations.Events {
    public interface IEventBus {
        void Send(IEvent @event);
    }
}
namespace ProstoA.Operations.Commands {
    public interface ICommand : IOperation {}

    public interface ICommand<out TOut> : IOperation { }
}
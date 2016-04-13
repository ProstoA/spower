namespace ProstoA.Operations.Commands {
    public interface ICommandHandler<in T, out TResult> : IOperationHandler<T, IOperationResult<TResult>>
        where T : ICommand<TResult> {
    }

    public interface ICommandHandler<in T> : IOperationHandler<T, IOperationResult>
        where T : ICommand {
    }
}
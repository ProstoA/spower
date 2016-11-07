namespace ProstoA.Operations.Commands {
    public interface ICommandHandler<in T> : IOperationHandler<T, IOperationResult>
        where T: ICommand { }

    public interface ICommandHandler<in T, out TResult> : IOperationHandler<T, IOperationResult<TResult>>
        where T: IResultCommand<TResult> { }
}
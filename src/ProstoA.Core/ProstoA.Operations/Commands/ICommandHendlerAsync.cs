using System.Threading.Tasks;

namespace ProstoA.Operations.Commands {
    public interface ICommandHendlerAsync<in T, TResult> : IOperationHandler<T, Task<IOperationResult<TResult>>>
        where T: IResultCommand<TResult> { }

    public interface ICommandHendlerAsync<in T> : IOperationHandler<T, Task<IOperationResult>>
        where T: ICommand { }
}
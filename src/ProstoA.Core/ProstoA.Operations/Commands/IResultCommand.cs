using System.Threading.Tasks;

namespace ProstoA.Operations.Commands {
    public interface IResultCommand<out T> : ICommand { }

    public interface ICommandResultService : ICommandService {
        Task<TResult> Execute<TResult>(IResultCommand<TResult> command);
    }

    public interface ICommandResultBus<in TCommand, in TResult> where TCommand : IResultCommand<TResult> {
        Task SendResult(TCommand command, TResult result);
    }
}
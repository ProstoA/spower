using System.Threading.Tasks;

namespace ProstoA.Operations.Commands {
    public interface ICommandService {
        void Execute(ICommand command);
    }

    public interface IAsyncCommandService {
        Task ExecuteAsync(ICommand command);
    }
}
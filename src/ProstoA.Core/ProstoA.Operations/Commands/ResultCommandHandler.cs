using System;
using System.Threading.Tasks;

namespace ProstoA.Operations.Commands {
    public abstract class ResultCommandHandler<TCommand, TResult> : ICommandHendlerAsync<TCommand> where TCommand : IResultCommand<TResult> {
        private readonly ICommandResultBus<TCommand, TResult> _resultBus;

        protected ResultCommandHandler(ICommandResultBus<TCommand, TResult> resultBus) {
            _resultBus = resultBus;
        }

        public abstract Task<IOperationResult<TResult>> Execute(TCommand command);

        public async Task<IOperationResult> Execute(TCommand command, ILogger logger) {
            try {
                var result = await Execute(command);
                await _resultBus.SendResult(command, result.Data);

                return new OperationResult();
            }
            catch (Exception ex) {
                return new OperationResult(new OperationError(ex));
            }

        }
    }
}
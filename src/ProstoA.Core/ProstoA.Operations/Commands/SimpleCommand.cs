using System;
using System.Diagnostics;

namespace ProstoA.Operations.Commands {
    public abstract class SimpleCommand<T, TResult> : ICommand<TResult>, ICommandHandler<T, TResult> where T : ICommand<TResult> {
        public abstract TResult Execute(ILogger logger);

        IOperationResult<TResult> IOperationHandler<T, IOperationResult<TResult>>.Execute(T command, ILogger logger) {
            Debug.Assert(Equals(command));

            try {
                return new OperationResult<TResult>(Execute(logger));
            }
            catch(Exception ex) {
                return new OperationResult<TResult>(new OperationError(ex));
            }
        }
    }

    public abstract class SimpleCommand<T> : ICommand, ICommandHandler<T> where T : ICommand {
        public abstract void Execute(ILogger logger);

        IOperationResult IOperationHandler<T, IOperationResult>.Execute(T command, ILogger logger) {
            Debug.Assert(Equals(command));

            try {
                Execute(logger);
                return new OperationResult();
            }
            catch (Exception ex) {
                return new OperationResult(new OperationError(ex));
            }
        }
    }
}
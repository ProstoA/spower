using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProstoA.Operations {
    public interface IOperationResult<out TResult> : IOperationResult {
        TResult Data { get; }
    }

    public interface IOperationResult {
        bool Success { get; }

        IOperationError Error { get; }
    }

    public class OperationAsyncResult : Task {
        public OperationAsyncResult(Action action) : base(action) { }
        public OperationAsyncResult(Action action, CancellationToken cancellationToken) : base(action, cancellationToken) { }
        public OperationAsyncResult(Action action, TaskCreationOptions creationOptions) : base(action, creationOptions) { }
        public OperationAsyncResult(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions) : base(action, cancellationToken, creationOptions) { }
        public OperationAsyncResult(Action<object> action, object state) : base(action, state) { }
        public OperationAsyncResult(Action<object> action, object state, CancellationToken cancellationToken) : base(action, state, cancellationToken) { }
        public OperationAsyncResult(Action<object> action, object state, TaskCreationOptions creationOptions) : base(action, state, creationOptions) { }
        public OperationAsyncResult(Action<object> action, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions) : base(action, state, cancellationToken, creationOptions) { }
    }
}
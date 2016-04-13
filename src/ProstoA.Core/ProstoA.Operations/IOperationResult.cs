namespace ProstoA.Operations {
    public interface IOperationResult<out TResult> : IOperationResult {
        TResult Data { get; }
    }

    public interface IOperationResult {
        bool Success { get; }

        IOperationError Error { get; }
    }
}
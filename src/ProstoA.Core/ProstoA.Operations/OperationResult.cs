namespace ProstoA.Operations {
    public class OperationResult<T> : OperationResult, IOperationResult<T> {
        public OperationResult(T data) {
            Data = data;
        }

        public OperationResult(IOperationError error) : base(error) {}

        public T Data { get; }
    }

    public class OperationResult : IOperationResult {
        public OperationResult() { }

        public OperationResult(IOperationError error) {
            Error = error;
        }

        public bool Success => Error == null;

        public IOperationError Error { get; }
    }
}
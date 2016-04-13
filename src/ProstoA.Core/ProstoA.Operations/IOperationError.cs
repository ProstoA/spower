namespace ProstoA.Operations {
    public interface IOperationError {
        string Message { get; }

        string Detals { get; }

        IOperationError[] InnerErrors { get; }
    }
}
using System;

namespace ProstoA.Operations {
    public class OperationError : IOperationError {
        public OperationError(string message, params IOperationError[] innerErrors) {
            Message = message;
            InnerErrors = innerErrors;
        }

        public OperationError(Exception exception) {
            Message = exception.Message;
            Detals = exception.ToString();
        }

        public string Message { get; }

        public string Detals { get; }

        public IOperationError[] InnerErrors { get; }
    }
}
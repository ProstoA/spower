using System;

namespace ProstoA.Data.Validaton {
    public interface IValidationError {
        string Name { get; }

        string Message { get; }

        Exception InnerException { get; }
    }
}
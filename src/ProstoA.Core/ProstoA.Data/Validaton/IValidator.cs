using System.Collections.Generic;

namespace ProstoA.Data.Validaton {
    public interface IValidator<T> {
        IEnumerable<IValidationError> Check();
    }
}
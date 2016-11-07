using System.Collections.Generic;

namespace ProstoA.Data.Metamodel {
    public interface IObjectRevision<T> {
        IReadOnlyCollection<IObjectIdentity<IObjectRevision<T>>> Parents { get; }
    }
}
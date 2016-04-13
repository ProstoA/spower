using System.Collections.Generic;

namespace ProstoA.Data.Object.Abstractions {
    public interface IObjectRevision<T> {
        IReadOnlyCollection<IObjectIdentity<IObjectRevision<T>>> Parents { get; }
    }
}
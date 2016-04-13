using System.Collections.Generic;

namespace ProstoA.Data.Model.Abstractions {
    public interface IHierarchical<out T> {
        IEnumerable<T> Parents { get; }
    }
}
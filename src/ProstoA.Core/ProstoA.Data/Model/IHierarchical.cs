using System.Collections.Generic;

namespace ProstoA.Data.Model {
    public interface IHierarchical<out T> {
        IEnumerable<T> Parents { get; }
    }
}
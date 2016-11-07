using System;
using System.Collections.Generic;

namespace ProstoA.Data.Store.Mappers {
    public interface IMappingOptions : IEnumerable<object> {
        object Get(Type type, object defaultValue = null);

        IMappingOptions Extend(IMappingOptions options);
    }
}
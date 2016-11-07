using System.Collections.Generic;

namespace ProstoA.Data.Store.Mappers {
    public interface IMapperResolver<TTo> {
        TTo Map<TFrom>(TFrom item, IMappingOptions options = null, TTo defaultValue = default(TTo));

        IEnumerable<TTo> Map<TFrom>(IEnumerable<TFrom> items, IMappingOptions options = null, IEnumerable<TTo> defaultValue = null);
    }
}
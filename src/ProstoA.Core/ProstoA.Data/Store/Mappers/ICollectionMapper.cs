using System.Collections.Generic;

namespace ProstoA.Data.Store.Mappers {
    public interface ICollectionMapper<in TFrom, out TTo> : IMapper<IEnumerable<TFrom>, IEnumerable<TTo>> { }
}
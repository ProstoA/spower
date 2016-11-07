using System;
using System.Collections.Generic;
using System.Linq;

namespace ProstoA.Data.Store.Mappers {

    // todo: вынести в сборку c DI

    public class MapperResolver<TTo> : IMapperResolver<TTo> {
        private readonly IServiceProvider _serviceProvider;

        public MapperResolver(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public TTo Map<TFrom>(TFrom item, IMappingOptions options = null, TTo defaultValue = default(TTo)) {
            if (item == null) {
                return defaultValue;
            }

            // todo: заюзать вызов экстеншена
            var mapper = (IMapper<TFrom, TTo>)_serviceProvider.GetService(typeof(IMapper<TFrom, TTo>));
            return mapper == null ? defaultValue : mapper.Map(item, options ?? new MappingOptions());
        }

        public IEnumerable<TTo> Map<TFrom>(IEnumerable<TFrom> items, IMappingOptions options = null, IEnumerable<TTo> defaultValue = null) {
            options = options ?? new MappingOptions();
            defaultValue = defaultValue ?? Enumerable.Empty<TTo>();

            if (items == null) {
                return defaultValue;
            }

            // todo: заюзать вызов экстеншена
            var collectionMapper = (ICollectionMapper<TFrom, TTo>)_serviceProvider.GetService(typeof(ICollectionMapper<TFrom, TTo>));

            if (collectionMapper != null) {
                return collectionMapper.Map(items, options);
            }

            // todo: заюзать вызов экстеншена
            var itemMapper = (IMapper<TFrom, TTo>)_serviceProvider.GetService(typeof(IMapper<TFrom, TTo>));
            return itemMapper == null ? defaultValue : items.Where(x => x != null).Select(x => itemMapper.Map(x, options));
        }
    }
}
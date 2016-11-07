using System;

namespace ProstoA.Data.Store.Mappers {
    public class MappingService : IMappingService {
        private readonly IServiceProvider _serviceProvider;

        public MappingService(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public IMapperResolver<TTo> To<TTo>() {
            return new MapperResolver<TTo>(_serviceProvider);
        }
    }
}
namespace ProstoA.Data.Store.Mappers {
    public interface IMappingService {
        IMapperResolver<TTo> To<TTo>();
    }
}
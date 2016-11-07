namespace ProstoA.Data.Store.Mappers {
    public interface IMapper<in TFrom, out TTo> {
        TTo Map(TFrom item, IMappingOptions options);
    }
}
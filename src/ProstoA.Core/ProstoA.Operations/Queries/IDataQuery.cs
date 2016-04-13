using ProstoA.Data.Model.Abstractions;

namespace ProstoA.Operations.Queries {
    public interface IDataQuery<TResult> {
        // Specificatin (какие строки возвращать, критерий отбора) part key, item key

        IDataModel ProjectionModel { get; } // какие поля возвращать

        // Paging (skip, take)
    }
}
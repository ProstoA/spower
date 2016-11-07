using ProstoA.Data.Metamodel;
using ProstoA.Data.Model;

namespace ProstoA.Operations.Queries {
    public interface IDataQuery<TResult> {
        // Specificatin (какие строки возвращать, критерий отбора) part key, item key

        IDataModel ProjectionModel { get; } // какие поля возвращать

        // Paging (skip, take)
    }
}
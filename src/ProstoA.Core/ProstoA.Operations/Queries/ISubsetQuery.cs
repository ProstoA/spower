namespace ProstoA.Operations.Queries {
    public interface ISubsetQuery<out TResult> : IQuery<ISubset<TResult>> {
        int Offset { get; set; }

        int Count { get; set; }
    }
}
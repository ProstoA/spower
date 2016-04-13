namespace ProstoA.Operations {
    public interface ICollectionOperationResult<out TItem> : IOperationResult {
        int Offset { get; }

        TItem[] Data { get; }

        int Total { get; }
    }
}
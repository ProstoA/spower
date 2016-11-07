namespace ProstoA.Data.Model {
    public interface IShardingKey {
        string PartitionKey { get; } // Basket

        string RowKey { get; }
    }
}
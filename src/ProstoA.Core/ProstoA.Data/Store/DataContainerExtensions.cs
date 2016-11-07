namespace ProstoA.Data.Store {
    public static class DataContainerExtensions {
        public static object ReadField(this IDataContainer container, string field) {
            return container.Read(null, field)[field];
        }
    }
}

namespace ProstoA.Operations.Events {
    public class ResultEvent<T> : IResultEvent<T> {
        public ResultEvent(T data) {
            Data = data;
        }

        public T Data { get; }
    }
}
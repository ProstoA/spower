namespace ProstoA.Operations {
    public interface IOperationHandler<in TIn, out TOut> {
        TOut Execute(TIn parameters, ILogger logger);

        // CanExecute
    }
}
namespace ProstoA.Workflow {
    public interface IWorkflowStateHandler<in T> where T : IWorkflowState {
        void OnEnter(T state, IWorkflowContext context);

        void OnExit(T state, IWorkflowContext context);
    }
}
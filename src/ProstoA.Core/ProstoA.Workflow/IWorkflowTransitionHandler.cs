namespace ProstoA.Workflow {
    public interface IWorkflowTransitionHandler<in T> where T : IWorkflowTransition {
        bool CanExecute(T transition, IWorkflowContext context);

        void Execute(T transition, IWorkflowContext context);
    }
}
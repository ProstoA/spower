using System.Collections.Generic;

using ProstoA.Data.Model.Abstractions;
using ProstoA.Data.Object.Abstractions;
using ProstoA.Security;

namespace ProstoA.Workflow {
    /// <summary>
    /// Workflow Instance
    /// </summary>
    public interface IWorkflowContext {
        IDataObject Data { get; }

        IUserContext User { get; }

        IWorkflow Workflow { get; }

        WorkflowExecutionStatus ExecutionStatus { get; }

        IWorkflowState CurrentState { get; }

        void Next(IWorkflowState state);
    }

    public enum WorkflowExecutionStatus {
        /// <summary>
        /// Создан новый процесс
        /// </summary>
        New,

        /// <summary>
        /// Исполняется
        /// </summary>
        Working,

        /// <summary>
        /// Исполнение остановленно
        /// </summary>
        Suspended,

        /// <summary>
        /// Исполнение завершилось
        /// </summary>
        Terminated
    }

    // Working => Ready, Running, Waiting
    // Terminated => Completed, Error, Canceled

    /// <summary>
    /// Workflow Definition
    /// </summary>
    public interface IWorkflow {
        IObjectIdentity Name { get; }

        IObjectDisplay Display { get; }

        /// <summary>
        /// where IWorkflowState => null
        /// </summary>
        IEnumerable<IWorkflowTransition> InitialTransitions { get; }

        /// <summary>
        /// where IWorkflowTransition => empty
        /// </summary>
        IEnumerable<IWorkflowState> FinallyStates { get; }
    }

    /// Workflow Template

}
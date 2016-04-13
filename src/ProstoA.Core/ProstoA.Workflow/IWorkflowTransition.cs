using System.Collections.Generic;

using ProstoA.Data.Model.Abstractions;
using ProstoA.Data.Object.Abstractions;

namespace ProstoA.Workflow {
    public interface IWorkflowTransition {
        IObjectIdentity Name { get; }

        IObjectDisplay Display { get; }

        IDataObject Data { get; }

        IEnumerable<IWorkflowState> NextStates { get; }
    }
}
using System.Collections.Generic;

using ProstoA.Data.Model.Abstractions;
using ProstoA.Data.Object.Abstractions;

namespace ProstoA.Workflow {
    public interface IWorkflowState {
        IObjectIdentity Name { get; }

        IObjectDisplay Display { get; }

        IDataObject Data { get; }

        IEnumerable<IWorkflowTransition> NextTransitions { get; }
    }
}
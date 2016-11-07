using System.Collections.Generic;

using ProstoA.Data.Metamodel;
using ProstoA.Data.Model;

namespace ProstoA.Workflow {
    public interface IWorkflowState {
        IObjectIdentity Name { get; }

        IObjectDisplay Display { get; }

        IDataObject Data { get; }

        IEnumerable<IWorkflowTransition> NextTransitions { get; }
    }
}
using System;

namespace ProstoA.Documents.Model {
    public interface IDocument {
        string Name { get; }

        string Title { get; }

        string CreatedBy { get; }

        DateTimeOffset? Created { get; }

        string ModifiedBy { get; }

        DateTimeOffset? Modified { get; }
    }
}
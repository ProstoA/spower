using System;

namespace ProstoA.Documents.Model {
    public interface IDocument {
        string Title { get; }

        string CreatedBy { get; }

        DateTimeOffset? Created { get; }

        string LastModifiedBy { get; }

        DateTimeOffset? LastModified { get; }
    }
}
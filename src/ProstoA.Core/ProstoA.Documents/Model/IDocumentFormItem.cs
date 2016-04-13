using System.Collections.Generic;

using ProstoA.Documents.Presentation.Abstractions;

namespace ProstoA.Documents.Model {
    public interface IFormItem {
        string Name { get; }

        string Title { get; }

        bool Selected { get; }

        bool Disabled { get; }

        bool Hidden { get; }

        IDocumentLayout Layout { get; }

        IEnumerable<IFormItem> Items { get; }
    }
}
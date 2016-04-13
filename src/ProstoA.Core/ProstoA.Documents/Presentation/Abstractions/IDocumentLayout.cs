using System.Collections.Generic;

namespace ProstoA.Documents.Presentation.Abstractions {
    public interface IDocumentLayout {
        IEnumerable<DocumentLayoutUnit> Columns { get; set; }
        IEnumerable<DocumentLayoutUnit> Rows { get; set; }
    }
}
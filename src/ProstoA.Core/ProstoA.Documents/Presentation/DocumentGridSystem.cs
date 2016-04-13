using System.Collections.Generic;

using ProstoA.Documents.Presentation.Abstractions;

namespace ProstoA.Documents.Presentation {
    public class DocumentGridSystem : IDocumentLayout {
        public IEnumerable<DocumentLayoutUnit> Columns { get; set; }

        public IEnumerable<DocumentLayoutUnit> Rows { get; set; }
    }
}
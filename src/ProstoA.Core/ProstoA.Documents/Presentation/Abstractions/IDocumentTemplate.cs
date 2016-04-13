using ProstoA.Documents.Model;

namespace ProstoA.Documents.Presentation.Abstractions {
    public interface IDocumentTemplate<in TDocument, in TForm> where TForm : IForm<TDocument> {
        IDocumentView Apply(TDocument document, TForm form);
    }
}
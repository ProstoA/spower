using System.IO;

namespace ProstoA.Documents.Presentation.Abstractions {
    public interface IDocumentView {
        string Name { get; }

        string Title { get; }

        string ContentType { get; }

        string FileExtension { get; }

        void Write(Stream stream);
    }
}
using System.IO;

namespace ProstoA.Data.Presentation {
    public abstract class FileView : IView {
        public abstract string Name { get; }

        public abstract string Title { get; }

        public abstract string ContentType { get; }

        public abstract string FileExtension { get; }

        public abstract void Write(Stream stream);
    }
}
using System.IO;

namespace ProstoA.Data.View {
    public abstract class FileView : ViewBase<Stream> {
        public abstract string Name { get; }

        public abstract string ContentType { get; }

        public abstract string FileExtension { get; }

        protected abstract void Write(Stream stream);

        public void WriteTo(Stream stream) {
            Stream.Synchronized(State).CopyTo(stream);
        }

        protected override Stream CreateState() {
            var ms = new MemoryStream();
            Write(ms);
            ms.Position = 0;
            return ms;
        }
    }
}
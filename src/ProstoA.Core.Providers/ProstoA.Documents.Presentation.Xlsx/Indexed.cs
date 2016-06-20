namespace ProstoA.Documents.Presentation.Xlsx {
    public struct Indexed<T> {
        public Indexed(int index, T value) {
            Index = index;
            Value = value;
        }

        public int Index { get; }

        public T Value { get; }
    }
}
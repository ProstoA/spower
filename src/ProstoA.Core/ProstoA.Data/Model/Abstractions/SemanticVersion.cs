namespace ProstoA.Data.Model.Abstractions {
    public sealed class SemanticVersion {
        public int Major { get; set; }

        public int Minor { get; set; }

        public int Patch { get; set; }

        public string Pre { get; set; }

        public string Build { get; set; }
    }
}
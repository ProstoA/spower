using System;
using System.Collections.Generic;

namespace ProstoA.Documents.Model {
    public class TabularDocument<T> : IDocument {
        public TabularDocument(string name, string title, IEnumerable<T> data) {
            Name = name;
            Title = title;
            Data = data;
        }

        public string Name { get; }

        public string Title { get; }

        public string CreatedBy { get; set; }

        public DateTimeOffset? Created { get; set; }

        public string ModifiedBy { get; set; }

        public DateTimeOffset? Modified { get; set; }

        public IEnumerable<T> Data { get; }
    }
}
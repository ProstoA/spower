using System;
using System.Collections.Generic;

namespace ProstoA.Documents.Model {
    public class TabularDocument<T> : IDocument {
        public TabularDocument(string title, IEnumerable<T> data) {
            Title = title;
            Data = data;
        }

        public string Title { get; }

        public string CreatedBy { get; set; }

        public DateTimeOffset? Created { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTimeOffset? LastModified { get; set; }

        public IEnumerable<T> Data { get; }

        public IEnumerable<TabularDocumentFields> Items { get; set; }
    }
}
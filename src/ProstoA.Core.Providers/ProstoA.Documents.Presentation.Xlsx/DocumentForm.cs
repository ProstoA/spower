using System.Collections.Generic;
using System.Linq;

using ProstoA.Documents.Model;

namespace ProstoA.Documents.Presentation.Xlsx {
    public class DocumentForm<TDocument, TFormItem> : IForm<TDocument> where TFormItem : IFormItem {
        public DocumentForm(string name, string title, params TFormItem[] items) {
            Name = name;
            Title = title;
            Items = items;
        }

        public string Name { get; }

        public string Title { get; }

        public IEnumerable<TFormItem> Items { get; }

        IEnumerable<IFormItem> IForm<TDocument>.Items => Items.Select(x => (IFormItem)x);
    }
}
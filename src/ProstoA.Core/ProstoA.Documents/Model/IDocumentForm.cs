using System.Collections.Generic;

namespace ProstoA.Documents.Model {
    public interface IForm<out T> {
        string Name { get; }

        string Title { get; }

        IEnumerable<IFormItem> Items { get; }
    }
}
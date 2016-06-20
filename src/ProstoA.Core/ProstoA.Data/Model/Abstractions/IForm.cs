using System.Collections.Generic;

namespace ProstoA.Data.Model.Abstractions {
    public interface IForm<out T> {
        string Name { get; }

        string Title { get; }

        IEnumerable<IFormItem> Items { get; }
    }
}
using System.Collections.Generic;

namespace ProstoA.Data.Model.Abstractions {
    public interface IFormItem {
        string Name { get; }

        string Title { get; }

        bool Selected { get; }

        bool Disabled { get; }

        bool Hidden { get; }

        IEnumerable<IFormItem> Items { get; }
    }
}
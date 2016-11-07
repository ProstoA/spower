using System.Collections.Generic;

namespace ProstoA.Data.Metamodel {
    public interface IFormItem {
        string Name { get; }

        string Title { get; }

        bool Selected { get; }

        bool Disabled { get; }

        bool Hidden { get; }

        IEnumerable<IFormItem> Items { get; }
    }
}
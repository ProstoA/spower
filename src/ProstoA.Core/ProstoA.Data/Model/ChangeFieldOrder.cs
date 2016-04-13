using System.Collections.Generic;

using ProstoA.Data.Model.Abstractions;

namespace ProstoA.Data.Model {
    public class ChangeFieldOrder : IDataModelFix {
        public ChangeFieldOrder(string name) {
            Name = name;
        }

        public string Name { get; }

        public IEnumerable<IDataModel> Applay(IEnumerable<IDataModel> items) {
            IDataModel t = null;

            foreach (var item in items) {
                if (item.Identity.Name != Name) {
                    yield return item;
                    continue;
                }

                t = item;
            }

            if (t != null) {
                yield return t;
            }
        }
    }
}
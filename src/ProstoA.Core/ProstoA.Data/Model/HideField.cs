using System.Collections.Generic;

using ProstoA.Data.Model.Abstractions;

namespace ProstoA.Data.Model {
    public class HideField : IDataModelFix {
        public HideField(string name) {
            Name = name;
        }

        public string Name { get; }

        public IEnumerable<IDataModel> Applay(IEnumerable<IDataModel> items) {
            foreach (var item in items) {
                if (item.Identity.Name != Name) {
                    yield return item;
                    continue;
                }

                var fieldArgs = item.GetType().GenericTypeArguments;
                var fieldType = typeof(DataField<>).MakeGenericType(fieldArgs);

                var title = item.Display.Title;
                var constraints = (DataConstraints)fieldType.GetProperty("Constraints").GetValue(item);
                constraints = (DataConstraints)fieldType.GetMethod("Clone").Invoke(item, new object[0]);
                constraints.ShowOptions = ShowOptions.Hide;

                var fieldConstructor = fieldType.GetConstructor(new[] { typeof(string), typeof(string), typeof(DataConstraints) });
                var field = fieldConstructor.Invoke(new object[] { Name, title, constraints });

                yield return (IDataModel)field;
            }
        }
    }
}
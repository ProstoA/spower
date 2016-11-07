using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ProstoA.Data.Metamodel {
    [Serializable]
    public class PropertyDataItemModel : ISimpleDataModel, IObjectDisplay {
        public PropertyDataItemModel(IDataModel parent, PropertyInfo prop) {
            Parents = new[] { parent.Identity };

            Name = prop.Name;
            Readonly = !prop.CanWrite;

            var dataType = prop.GetCustomAttribute<DataTypeAttribute>()
                ?? new DataTypeAttribute(System.ComponentModel.DataAnnotations.DataType.Text);

            DataType = dataType.DataType == System.ComponentModel.DataAnnotations.DataType.Custom
                ? dataType.CustomDataType
                : dataType.DataType.ToString();

            Default = prop.GetCustomAttribute<DefaultValueAttribute>()?.Value?.ToString();

            var display = prop.GetCustomAttribute<DisplayAttribute>();

            Title = display?.Name ?? Name;
            Description = display?.Description;
            Prompt = display?.Prompt;
        }

        public IEnumerable<IObjectIdentity> Parents { get; }

        public string Name { get; }

        public string DataType { get; }

        public bool Readonly { get; }

        public string Default { get; }

        public string Title { get; }

        public string Description { get; }

        public string Prompt { get; }

        IObjectIdentity IDataModel.Identity => new SimpleObjectIdentity(Name);

        IObjectDisplay IDataModel.Display => this;
    }
}
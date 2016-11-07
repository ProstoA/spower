using System;

namespace ProstoA.Data.Metamodel {
    [Serializable]
    public class StringDataObject : ISimpleDataObject {
        public StringDataObject(ISimpleDataModel model, string value) {
            Value = value;
            Model = model;
        }

        public string Value { get; }

        public ISimpleDataModel Model { get; }

        IDataModel IDataObject.Model => Model;

        public IObjectIdentity Identity => null;
    }

    public class ObjectDisplay : IObjectDisplay {
        public ObjectDisplay(string title, string description = null) {
            Title = title;
            Description = description;
        }

        public string Title { get; }

        public string Description { get; }
    }
}
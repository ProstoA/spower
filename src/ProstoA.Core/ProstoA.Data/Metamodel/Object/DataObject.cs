using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using ProstoA.Data.Model;
using ProstoA.Data.Store;

namespace ProstoA.Data.Metamodel {
    public class DataObject : IComplexDataObject {
        private IDataAccessor _dataAccessor;
        private IDataMutator _dataMutator;

        public DataObject(object data) {
            _dataAccessor = new TypeDataAccessor(data);
            _dataMutator = new TypeDataAccessor(data);
            _model = new DataModel(data.GetType());
        }

        public DataObject(Type type) {
            _dataAccessor = new TypeDataAccessor(type);
            _dataMutator = new TypeDataAccessor(type);
            _model = new DataModel(type);
        }

        private DataModel _model { get; set; }

        private Dictionary<string, string> _data {
            get { return _dataAccessor.ToDictionary(); }
            set { _dataAccessor = new DictionaryDataAccessor(value); }
        }

        public IDataObject this[string name] {
            get { return new StringDataObject((ISimpleDataModel)Model[name], _dataAccessor.GetValue(name)); }
            set { _dataMutator.SetValue(name, ((ISimpleDataObject)value).Value); }
        }

        public IComplexDataModel Model => _model;

        IDataModel IDataObject.Model => _model;

        [XmlIgnore]
        public IObjectIdentity Identity => null;
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ProstoA.Data.Metamodel {
    public interface IDataItem<T> : IObjectIdentity<IDataItem<T>> {
        string Name { get; }

        IDataItemValueType ValueType { get; }
    }

    public interface IDataItemValueType {
        IReadOnlyDictionary<Type, IDataItemValueConstraint> Constraints { get; }

        void DefineConstraint(IDataItemValueConstraint constraint, bool isOverride = true);
    }

    public interface IDataItemValueTypeConverter<T, TResult> {
        TResult ConvertTo(T value);

        T ConvertFrom(TResult value);
    }

    public interface IDataItemValueConstraint { }

    public interface IDataItemValueValidationConstraint : IDataItemValueConstraint {
        bool Check(object value);
    }

    public interface IDataItemValueTransformationConstraint : IDataItemValueConstraint {
        object Apply(object value);
    }

    public class RequiredValueConstraint : IDataItemValueValidationConstraint, IDataItemValueTransformationConstraint {
        public object DefaultValue { get; set; }

        public RequiredValueConstraint(object defaultValue = null) {
            DefaultValue = defaultValue;
        }

        public bool Check(object value) {
            return value != null;
        }

        public object Apply(object value) {
            return value ?? DefaultValue;
        }
    }

    public class StringLengthValueConstraint : IDataItemValueValidationConstraint {
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }

        public StringLengthValueConstraint(int maxLength) {
            MaxLength = maxLength;
        }

        public StringLengthValueConstraint(int minLength, int maxLength) {
            MinLength = minLength;
            MaxLength = maxLength;
        }

        public bool Check(object value) {
            throw new NotImplementedException();
        }
    }

    public class DataItemValueType : IDataItemValueType {
        private readonly Dictionary<Type, IDataItemValueConstraint> _constraints = new Dictionary<Type, IDataItemValueConstraint>();

        public IReadOnlyDictionary<Type, IDataItemValueConstraint> Constraints => new ReadOnlyDictionary<Type, IDataItemValueConstraint>(_constraints);

        protected void DefineConstraint(IDataItemValueConstraint constraint, bool isOverride = true) {
            ((IDataItemValueType)this).DefineConstraint(constraint, isOverride);
        }

        void IDataItemValueType.DefineConstraint(IDataItemValueConstraint constraint, bool isOverride) {
            var constraintType = constraint.GetType();

            if(!_constraints.ContainsKey(constraintType)) {
                _constraints.Add(constraintType, constraint);
                return;
            }

            if(!isOverride) {
                throw new InvalidOperationException("An constraint with the same type has already been added.");
            }

            _constraints[constraintType] = constraint;
        }
    }
}
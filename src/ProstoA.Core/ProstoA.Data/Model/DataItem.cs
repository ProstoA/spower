using System;
using System.Linq;
using System.Linq.Expressions;

using ProstoA.Data.Model.Abstractions;

namespace ProstoA.Data.Model {
    public class DataItem<TParent, TValue> : DataItem<TParent> {
        public DataItem(string name, IDataItemValueType valueType) : base(name, valueType) {}
    }

    public class DataItem<TParent> : IDataItem<TParent> {
        protected DataItem(string name, IDataItemValueType valueType) {
            Name = name;
            ValueType = valueType;
        }

        public string Name { get; }

        public IDataItemValueType ValueType { get; }

        string Object.Abstractions.IObjectIdentity<IDataItem<TParent>>.Key => Name;
        string Object.Abstractions.IObjectIdentity<IDataItem<TParent>>.Type => GetType().AssemblyQualifiedName;

        protected bool Equals(DataItem<TParent> other) {
            return string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj) {
            if(ReferenceEquals(null, obj)) {
                return false;
            }
            if(ReferenceEquals(this, obj)) {
                return true;
            }
            if(obj.GetType() != this.GetType()) {
                return false;
            }
            return Equals((DataItem<TParent>)obj);
        }

        public override int GetHashCode() {
            return Name?.GetHashCode() ?? 0;
        }

        public static DataItem<TParent, T> Property<T>(Expression<Func<TParent, T>> prop, IDataItemValueType type = null) {
            return new DataItem<TParent, T>(GetName(prop.Body), type ?? ResolveValueType(typeof(T)));
        }

        private static string GetName(Expression prop) {
            var propExpr = prop as MemberExpression;
            if(propExpr == null) {
                throw new NotSupportedException(prop.NodeType.ToString());
            }

            if (propExpr.Expression.NodeType == ExpressionType.Parameter) {
                return propExpr.Member.Name;
            }

            return GetName(propExpr.Expression) + "." + propExpr.Member.Name;
        }

        private static IDataItemValueType ResolveValueType(Type valueType) {
            if(typeof(IDataItemValueType).IsAssignableFrom(valueType)) {
                return (IDataItemValueType)Activator.CreateInstance(valueType);
            }

            // Map CLR type

            if(valueType == typeof(string)) {
                return new StringValueType();
            }

            if(valueType == typeof(int)) {
                return new NumberValueType().Required();
            }

            if(valueType == typeof(int?)) {
                return new NumberValueType();
            }

            if(valueType == typeof(DateTimeOffset) || valueType == typeof(DateTime)) {
                return new DateTimeValueType().Required();
            }

            if(valueType == typeof(DateTimeOffset?) || valueType == typeof(DateTime?)) {
                return new DateTimeValueType();
            }

            // Convertible type

            var converter = valueType.GetInterfaces().FirstOrDefault(x =>
                x.Name == typeof(IDataItemValueTypeConverter<,>).Name && x.GenericTypeArguments[0] == valueType
            );

            if(converter == null) {
                // to JSON
                return new StringValueType();
            }

            // recursive resolve
            return ResolveValueType(converter.GenericTypeArguments[1]);
        }
    }

    public class BooleanValueType : DataItemValueType { }

    public class NumberValueType : DataItemValueType { }

    public class StringValueType : DataItemValueType { }

    public class DateTimeValueType : DataItemValueType { }

    public static class DataItemValueTypeExtensions {
        public static T Required<T>(this T valueType, object defaultValue = null) where T : IDataItemValueType {
            valueType.DefineConstraint(new RequiredValueConstraint(defaultValue));
            return valueType;
        }

        public static T Length<T>(this T valueType, int minLength, int maxLength) where T : StringValueType, IDataItemValueType {
            valueType.DefineConstraint(new StringLengthValueConstraint(minLength, maxLength));
            return valueType;
        }

        public static T Length<T>(this T valueType, int maxLength) where T : StringValueType, IDataItemValueType {
            valueType.DefineConstraint(new StringLengthValueConstraint(maxLength));
            return valueType;
        }
    }
}
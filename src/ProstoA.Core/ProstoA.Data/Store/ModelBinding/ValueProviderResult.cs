using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

using ProstoA.Data.Properties;

namespace ProstoA.Data.Store.ModelBinding {
    public class ValueProviderResult : IValueProviderResult {
        public ValueProviderResult(object rawValue, string attemptedValue, CultureInfo culture = null) {
            RawValue = rawValue;
            AttemptedValue = attemptedValue;
            Culture = culture ?? CultureInfo.InvariantCulture;
        }

        public object RawValue { get; }

        public string AttemptedValue { get; }

        public CultureInfo Culture { get; }

        public virtual object ConvertTo(Type type, CultureInfo culture = null) {
            if(type == null) {
                throw new ArgumentNullException(nameof(type));
            }

            return UnwrapPossibleArrayType(culture ?? Culture, RawValue, type);
        }

        private static object UnwrapPossibleArrayType(CultureInfo culture, object value, Type destinationType) {
            if(value == null || destinationType.IsInstanceOfType(value)) {
                return value;
            }

            // array conversion results in four cases, as below
            var valueAsArray = value as Array;
            if(destinationType.IsArray) {
                Type destinationElementType = destinationType.GetElementType();
                if(valueAsArray != null) {
                    // case 1: both destination + source type are arrays, so convert each element
                    IList converted = Array.CreateInstance(destinationElementType, valueAsArray.Length);
                    for(var i = 0; i < valueAsArray.Length; i++) {
                        converted[i] = ConvertSimpleType(culture, valueAsArray.GetValue(i), destinationElementType);
                    }
                    return converted;
                } else {
                    // case 2: destination type is array but source is single element, so wrap element in array + convert
                    object element = ConvertSimpleType(culture, value, destinationElementType);
                    IList converted = Array.CreateInstance(destinationElementType, 1);
                    converted[0] = element;
                    return converted;
                }
            } else if(valueAsArray != null) {
                // case 3: destination type is single element but source is array, so extract first element + convert
                if(valueAsArray.Length > 0) {
                    value = valueAsArray.GetValue(0);
                    return ConvertSimpleType(culture, value, destinationType);
                } else {
                    // case 3(a): source is empty array, so can't perform conversion
                    return null;
                }
            }
            // case 4: both destination + source type are single elements, so convert
            return ConvertSimpleType(culture, value, destinationType);
        }

        private static object ConvertSimpleType(CultureInfo culture, object value, Type destinationType) {
            if (value == null || destinationType.IsInstanceOfType(value)) {
                return value;
            }

            var valueAsString = value as string;
            if (valueAsString != null && valueAsString.Trim().Length == 0) {
                return null;
            }

            TypeConverter converter = TypeDescriptor.GetConverter(destinationType);
            var canConvertFrom = converter.CanConvertFrom(value.GetType());
            if (!canConvertFrom) {
                converter = TypeDescriptor.GetConverter(value.GetType());
            }

            if (!(canConvertFrom || converter.CanConvertTo(destinationType))) {
                // EnumConverter cannot convert integer, so we verify manually
                if (destinationType.IsEnum && value is int) {
                    return Enum.ToObject(destinationType, (int) value);
                }

                // In case of a Nullable object, we try again with its underlying type.
                Type underlyingType = Nullable.GetUnderlyingType(destinationType);
                if (underlyingType != null) {
                    return ConvertSimpleType(culture, value, underlyingType);
                }

                var message = string.Format(
                    CultureInfo.CurrentCulture,
                    Resources.ValueProviderResult_NoConverterExists,
                    value.GetType().FullName,
                    destinationType.FullName
                    );

                throw new InvalidOperationException(message);
            }

            try {
                return canConvertFrom
                    ? converter.ConvertFrom(null, culture, value)
                    : converter.ConvertTo(null, culture, value, destinationType);

            }
            catch (Exception ex) {
                var message = string.Format(
                    CultureInfo.CurrentCulture,
                    Resources.ValueProviderResult_ConversionThrew,
                    value.GetType().FullName,
                    destinationType.FullName
                    );

                throw new InvalidOperationException(message, ex);
            }
        }
    }
}
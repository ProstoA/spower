using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ProstoA {
    public static class DictionaryExtensions {
        public static IDictionary<TKey, TValue> Extend<TKey, TValue>(this IDictionary<TKey, TValue> first, params IDictionary<TKey, TValue>[] seconds) {
            return new[] {first}.Concat(seconds).SelectMany(x => x)
                .GroupBy(x => x.Key, x => x.Value)
                .ToDictionary(x => x.Key, x => x.Last());
        }

        public static IDictionary<TKey, TValue> ByDefault<TKey, TValue>(this IDictionary<TKey, TValue> item, IDictionary<TKey, TValue> defaults) {
            return defaults.Extend(item);
        }

        public static string ToTitleCase(this string value) {
            return string.IsNullOrEmpty(value)
                ? string.Empty
                : string.Concat(value.Split(' ', '-').Where(x => x.Length > 0).Select(x => char.ToUpper(x[0]) + x.Substring(1)));
        }

        public static string ToCamelCase(this string value) {
            value = value.ToTitleCase();
            return value.Length > 0 ? char.ToLower(value[0]) + value.Substring(1) : string.Empty;
        }

        public static IDictionary<string, object> ConvertToDictionary(this object values) {
            return TypeDescriptor.GetProperties(values)
                .OfType<PropertyDescriptor>()
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(values));
        }
    }
}
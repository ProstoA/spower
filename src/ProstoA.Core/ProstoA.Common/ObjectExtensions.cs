using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace ProstoA {
    public static class CollectionExtensions {
        public static int? IndexOf<T>(this IEnumerable<T> items, Func<T, bool> predicate) {
            return items.Select((value, index) => new { value, index }).FirstOrDefault(x => predicate(x.value))?.index;
        }
    }

    public static class ObjectExtensions {
        public static T Of<T>(this object obj) {
            return (T) obj;
        }

        public static bool Is<T>(this object obj) {
            return obj is T;
        }

        public static T As<T>(this object obj) where T : class {
            return obj as T;
        }

        public static TR IfHasValue<T, TR>(this T? obj, Func<T, TR> expr, TR defaultValue = default(TR))
            where T : struct {
            return obj.HasValue ? expr(obj.Value) : defaultValue;
        }

        public static TR Eval<T, TR>(this T obj, Func<T, TR> expr, TR defaultValue = default(TR)) where T : class {
            return ReferenceEquals(obj, null) ? defaultValue : expr(obj);
        }

        public static IEnumerable<TR> Eval<T, TR>(this T obj, Func<T, IEnumerable<TR>> expr) {
            return ReferenceEquals(obj, null) ? Enumerable.Empty<TR>() : expr(obj);
        }

        public static TR Safe<T, TR>(this T obj, Func<T, TR> expr, Func<ExceptionDispatchInfo, TR> fail) {
            try {
                return expr(obj);
            }
            catch (Exception ex) {
                return fail(ExceptionDispatchInfo.Capture(ex));
            }
        }

        public static TR Safe<T, TR>(this T obj, Func<T, TR> expr, TR defaultValue = default(TR)) {
            return Safe(obj, expr, x => defaultValue);
        }

        public static string StringJoin(this IEnumerable<string> first, string separator, params string[] second) {
            return string.Join(separator, first.Concat(second));
        }

        public static IEnumerable<TSource> Append<TSource>(this IEnumerable<TSource> first, params TSource[] second) {
            return first.Concat(second);
        }

        public static IEnumerable<T> AsMany<T>(this T item) {
            return new[] {item};
        }

        public static IDictionary<string, object> ConvertToDictionary(this object values) {
            return TypeDescriptor.GetProperties(values)
                .OfType<PropertyDescriptor>()
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(values));
        }
    }
}
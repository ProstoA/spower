using System;
using System.Globalization;

namespace ProstoA.Data.Store.ModelBinding {
    public interface IValueProviderResult {
        object RawValue { get; }

        string AttemptedValue { get; }

        CultureInfo Culture { get; }

        object ConvertTo(Type type, CultureInfo culture);
    }
}
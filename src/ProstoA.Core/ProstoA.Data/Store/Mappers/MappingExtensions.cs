using System.Collections.Generic;
using System.Linq;

namespace ProstoA.Data.Store.Mappers {
    public static class MappingExtensions {
        public static TTo[] MapToArray<TFrom, TTo>(this IMapperResolver<TTo> mapper, IEnumerable<TFrom> items, params object[] options) {
            var opt = new MappingOptions(options);
            return mapper.Map(items, opt).ToArray();
        }

        public static TTo Map<TFrom, TTo>(this IMapperResolver<TTo> mapper, TFrom item, params object[] options) {
            var opt = new MappingOptions(options);
            return mapper.Map(item, opt, opt.Get<TTo>());
        }
    }

    public static class MappingOptionsExtensions {
        public static T Get<T>(this IMappingOptions options, T defaultValue = default(T)) {
            return (T) options.Get(typeof (T), defaultValue);
        }

        public static IMappingOptions Extend(this IMappingOptions parent, params object[] options) {
            return parent.Extend(new MappingOptions(options));
        }
    }
}
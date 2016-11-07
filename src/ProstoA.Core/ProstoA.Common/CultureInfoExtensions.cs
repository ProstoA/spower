using System.Collections.Generic;
using System.Globalization;

namespace ProstoA {
    public static class CultureInfoExtensions {
        public class CultureInfoAndPriority {
            public CultureInfoAndPriority(CultureInfo cultureInfo, int priority) {
                CultureInfo = cultureInfo;
                Priority = priority;
            }

            public CultureInfo CultureInfo { get; }

            public int Priority { get; }
        }

        public static IEnumerable<CultureInfo> ThisAndParents(this CultureInfo cultureInfo) {
            while (!CultureInfo.InvariantCulture.Equals(cultureInfo)) {
                yield return cultureInfo;
                cultureInfo = cultureInfo.Parent;
            }

            yield return CultureInfo.InvariantCulture;
        }
    }
}
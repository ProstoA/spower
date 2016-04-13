using System.Collections.Generic;
using System.Linq;

using ProstoA.Data.Model.Abstractions;

namespace ProstoA.Data.Model {
    public class InheritDataSchema : DataSchema {
        public InheritDataSchema(IComplexDataModel parent, string name, string title, params IDataModelFix[] items)
            : base(name, title, ApplayFixes(parent, items)) {
        }

        private static IEnumerable<IDataModel> ApplayFixes(IComplexDataModel origin, IEnumerable<IDataModelFix> fixes) {
            var items = origin.Items.Select(x => origin[x]);
            return fixes.Aggregate(items, (x, fix) => fix.Applay(x));
        }
    }
}
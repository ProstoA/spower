using System;
using System.Collections.Generic;

namespace ProstoA.Data.Metamodel {
    public interface IDataModel {
        IEnumerable<IObjectIdentity> Parents { get; }

        IObjectIdentity Identity { get; } // Name

        IObjectDisplay Display { get; } // Title, Description

        //DataConstraints Constraints { get; }
    }

    public class DataConstraints {
        public bool Required { get; set; } = false;

        public bool SaveHistory { get; set; } = false;

        public int MaxLength { get; set; } = 0;

        public int LineCount { get; set; } = 1;

        public object[] ChoiceValues { get; set; } = new object[0];

        public object DefaultValue { get; set; } = null;

        public ShowOptions ShowOptions { get; set; } = ShowOptions.All;

        public DataConstraints Clone() {
            return (DataConstraints)MemberwiseClone();
        }
    }

    [Flags]
    public enum ShowOptions {
        Hide = 0,

        Display = 1,

        Edit = 2,

        New = 4,

        HideNew = Display | Edit,

        HideEdit = Display | New,

        All = Display | Edit | New
    }

}
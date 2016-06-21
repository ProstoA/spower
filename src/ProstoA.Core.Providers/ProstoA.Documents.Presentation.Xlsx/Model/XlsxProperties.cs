using System;

namespace ProstoA.Documents.Presentation.Xlsx.Model {
    public class XlsxProperties {
        public string Company { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? Created { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModified { get; set; }
    }
}
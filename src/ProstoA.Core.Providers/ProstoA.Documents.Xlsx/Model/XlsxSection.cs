using System.Collections.Generic;
using System.Linq;

namespace ProstoA.Documents.Xlsx.Model {
    public class XlsxSection {
        public XlsxSection(params IEnumerable<XlsxCell>[] data) {
            Data = data.SelectMany(x => x).ToArray();
        }

        public XlsxCell[] Data { get; set; }
    }
}
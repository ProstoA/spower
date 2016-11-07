using ProstoA.Documents.Xlsx.Model;

namespace ProstoA.Documents.Xlsx {
    public class XlsxFileContent {
        public XlsxWorksheet[] Worksheets { get; set; }

        public XlsxCellStyle[] Styles { get; set; }

        public string[] SharedStrings { get; set; }

        public XlsxProperties Properties { get; set; }
    }
}
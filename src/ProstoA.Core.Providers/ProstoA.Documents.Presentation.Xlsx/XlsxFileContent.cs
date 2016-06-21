using ProstoA.Documents.Presentation.Xlsx.Model;

namespace ProstoA.Documents.Presentation.Xlsx {
    public class XlsxFileContent {
        public Indexed<XlsxWorksheet>[] Worksheets { get; set; }

        public Indexed<XlsxCellStyle>[] Styles { get; set; }

        public Indexed<string>[] SharedStrings { get; set; }

        public XlsxProperties Properties { get; set; }
    }
}
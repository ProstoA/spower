using DocumentFormat.OpenXml.Spreadsheet;

namespace ProstoA.Documents.Presentation.Xlsx.Model {
    public class XlsxCellValue {
        public XlsxCellValue(string value, CellValues valueType) {
            Value = value;
            ValueType = valueType;
        }

        public string Value { get; set; }

        public CellValues ValueType { get; set; }
    }
}
namespace ProstoA.Documents.Xlsx.Model {
    public class XlsxCell {
        public int Column { get; set; }

        public int ColumnSpan { get; set; } = 1;

        public int Row { get; set; }

        public int RowSpan { get; set; } = 1;

        public XlsxCellValue Data { get; set; }

        public int StyleIndex { get; set; }
    }
}
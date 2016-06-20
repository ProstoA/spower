using DocumentFormat.OpenXml.Spreadsheet;

namespace ProstoA.Documents.Presentation.Xlsx.Model {
    public class XlsxCellStyle {
        public XlsxFontStyle FontStyle { get; set; } = XlsxFontStyle.None;
        public string FontFamily { get; set; } = "Calibri";
        public double FontSize { get; set; } = 9;
        public string FontCollorRgb { get; set; } = "FF000000";

        public XlsxFillStyle Fill { get; set; } = null;

        public HorizontalAlignmentValues HorizontalAlignment { get; set; } = HorizontalAlignmentValues.Left;
        public VerticalAlignmentValues VerticalAlignment { get; set; } = VerticalAlignmentValues.Top;
        public bool WrapText { get; set; }
        public bool FillText { get; set; }
        public int Indent { get; set; }
    }
}
using DocumentFormat.OpenXml.Spreadsheet;

namespace ProstoA.Documents.Presentation.Xlsx.Model {
    public class XlsxWorksheet {
        public string Title { get; set; }

        public bool IsActive { get; set; }

        public XlsxLayout Layout { get; set; }

        public XlsxSection[] Data { get; set; }

        public int ZoomScale { get; set; } = 100;

        public OrientationValues Orientation { get; set; }

        public SheetFormatProperties RowFormat { get; set; } = new SheetFormatProperties() {
            DefaultRowHeight = 15.75D,
            DyDescent = 0.25D
        };

        public PrintOptions PrintOptions { get; set; } = new PrintOptions { HorizontalCentered = true };

        public PageMargins PageMargins { get; set; } = new PageMargins {
            Left = 0.25D,
            Right = 0.25D,
            Top = 0.25D,
            Bottom = 0.25D,
            Header = 0.35D,
            Footer = 0.35D
        };
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

using ProstoA.Documents.Presentation.Abstractions;
using ProstoA.Documents.Presentation.Xlsx.Generators;

namespace ProstoA.Documents.Presentation.Xlsx {
    public class XlsxDocumentView : IDocumentView {
        public string Name { get; set; }

        public string Title { get; set; }

        string IDocumentView.ContentType => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        string IDocumentView.FileExtension => ".xlsx";

        public Indexed<XlsxWorksheet>[] Worksheets { get; set; }

        public Indexed<XlsxCellStyle>[] Styles { get; set; }

        public Indexed<string>[] SharedStrings { get; set; }

        public XlsxProperties Properties { get; set; }

        public void Write(Stream stream) {
            using(var package = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook)) {
                new ExtendedFilePropertiesPartGenerator().Do(package, this);
                var workbookPart = new WorkbookPartGenerator().Do(package, Worksheets);
                new WorksheetPartGenerator().Do(workbookPart, Worksheets);
                new WorkbookStylesPartGenerator().Do(workbookPart, Styles);
                new SharedStringTablePartGenerator().Do(workbookPart, SharedStrings);

                package.PackageProperties.Creator = Properties.CreatedBy;
                package.PackageProperties.Created = Properties.Created;
                package.PackageProperties.Modified = Properties.Modified;
                package.PackageProperties.LastModifiedBy = Properties.ModifiedBy;
            }
        }
    }

    public struct Indexed<T> {
        public Indexed(int index, T value) {
            Index = index;
            Value = value;
        }

        public int Index { get; }

        public T Value { get; }
    }

    public class XlsxWorksheet {
        public string Title { get; set; }

        public bool IsActive { get; set; }

        public IDocumentLayout Layout { get; set; }

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

    public class XlsxSection {
        public XlsxSection(params IEnumerable<XlsxCell>[] data) {
            Data = data.SelectMany(x => x).ToArray();
        }

        public XlsxCell[] Data { get; set; }
    }

    public class XlsxCell {
        public int Column { get; set; }

        public int ColumnSpan { get; set; } = 1;

        public int Row { get; set; }

        public int RowSpan { get; set; } = 1;

        public XlsxCellValue Data { get; set; }

        public int StyleIndex { get; set; }
    }

    public class XlsxCellValue {
        public XlsxCellValue(string value, CellValues valueType) {
            Value = value;
            ValueType = valueType;
        }

        public string Value { get; set; }

        public CellValues ValueType { get; set; }
    }

    public class XlsxProperties {
        public string Company { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? Created { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? Modified { get; set; }
    }

    public class XlsxCellStyle {
        public FontStyle FontStyle { get; set; } = FontStyle.None;
        public string FontFamily { get; set; } = "Calibri";
        public double FontSize { get; set; } = 9;
        public string FontCollorRgb { get; set; } = "FF000000";

        public FillStyle Fill { get; set; } = null;

        public HorizontalAlignmentValues HorizontalAlignment { get; set; } = HorizontalAlignmentValues.Left;
        public VerticalAlignmentValues VerticalAlignment { get; set; } = VerticalAlignmentValues.Top;
        public bool WrapText { get; set; }
        public bool FillText { get; set; }
        public int Indent { get; set; }
    }

    public class FillStyle {
        public string FillForegroundColorArgb { get; set; } = "FF000000";
        public string FillBackgroundColorArgb { get; set; }
    }

    [Flags]
    public enum FontStyle {
        None = 0,
        Italic = 1,
        Bold = 2,
        Underline = 4
    }
}
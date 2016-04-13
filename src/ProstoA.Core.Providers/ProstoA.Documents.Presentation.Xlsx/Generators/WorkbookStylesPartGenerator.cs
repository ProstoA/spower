using System;
using System.Linq;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ProstoA.Documents.Presentation.Xlsx.Generators {
    internal sealed class WorkbookStylesPartGenerator {
        public WorkbookStylesPart Do(WorkbookPart workbookPart, params Indexed<XlsxCellStyle>[] styles) {
            var fonts = styles.Select(x => new Font(
                Map(x.Value.FontStyle),
                new FontSize { Val = x.Value.FontSize },
                new Color { Rgb = x.Value.FontCollorRgb }, //{Theme = 1U}
                new FontName { Val = x.Value.FontFamily },
                new FontFamilyNumbering { Val = 2 },
                new FontCharSet { Val = 204 },
                new FontScheme { Val = FontSchemeValues.Minor }
            )).ToArray();

            var fills = new[] {
                new Fill(new PatternFill {PatternType = PatternValues.None}),
                new Fill(new PatternFill {PatternType = PatternValues.None})
            }.Concat(styles.Select(x => x.Value.Fill == null
                ? new Fill(new PatternFill { PatternType = PatternValues.None })
                : new Fill(new PatternFill(
                        new ForegroundColor { Rgb = x.Value.Fill.FillForegroundColorArgb }, //{Theme = 1U}
                        new BackgroundColor { Rgb = x.Value.Fill.FillBackgroundColorArgb } //{Theme = 1U}
                    ) {
                    PatternType = PatternValues.Solid
                })
            )).ToArray();

            var borders = new OpenXmlElement[] {
                new Border(
                    new LeftBorder(),
                    new RightBorder(),
                    new TopBorder(),
                    new BottomBorder(),
                    new DiagonalBorder()
                    ),
                new Border(
                    new LeftBorder(new Color {Theme = 1U}) {Style = BorderStyleValues.Thin},
                    new RightBorder(new Color {Theme = 1U}) {Style = BorderStyleValues.Thin},
                    new TopBorder(new Color {Theme = 1U}) {Style = BorderStyleValues.Thin},
                    new BottomBorder(new Color {Theme = 1U}) {Style = BorderStyleValues.Thin},
                    new DiagonalBorder(new Color {Theme = 1U}) {Style = BorderStyleValues.Thin}
                    ),
                new Border(
                    new LeftBorder(new Color {Theme = 1U}) {Style = BorderStyleValues.Thick},
                    new RightBorder(new Color {Theme = 1U}) {Style = BorderStyleValues.Thin},
                    new TopBorder(new Color {Theme = 1U}) {Style = BorderStyleValues.Thin},
                    new BottomBorder(new Color {Theme = 1U}) {Style = BorderStyleValues.Thin},
                    new DiagonalBorder(new Color {Theme = 1U}) {Style = BorderStyleValues.Thin}
                    )
            }; // Color color4 = new Color(){ Indexed = (UInt32Value)64U };

            var cellStyleFormats = new OpenXmlElement[] {
                new CellFormat() {NumberFormatId = 0U, FontId = 0U, FillId = 0U, BorderId = 0U}
            };

            var cellFormats = styles.Select(x => new CellFormat(
                new Alignment {
                    Horizontal = x.Value.HorizontalAlignment,
                    Vertical = x.Value.VerticalAlignment,
                    WrapText = x.Value.WrapText
                }) {
                    NumberFormatId = 0U,
                    FontId = (uint)x.Index,
                    FillId = (uint)x.Index + 2,
                    BorderId = 0U,
                    FormatId = 0U,
                    ApplyFont = true,
                    ApplyAlignment = true
                }
            ).ToArray();

            var cellStyles = new OpenXmlElement[] {
                new CellStyle {Name = "Обычный", FormatId = 0U, BuiltinId = 0U}
            };

            var differentialFormats = new OpenXmlElement[] { };

            var tableStyles = new OpenXmlElement[] { };

            var slicerStyleExtension = new StylesheetExtension() { Uri = "{EB79DEF2-80B8-43e5-95BD-54CBDDF9020C}" };
            slicerStyleExtension.AddNamespaceDeclaration("x14", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
            slicerStyleExtension.AppendChild(new DocumentFormat.OpenXml.Office2010.Excel.SlicerStyles { DefaultSlicerStyle = "SlicerStyleLight1" });

            //var timelineStyleExtension = new StylesheetExtension() { Uri = "{9260A510-F301-46a8-8635-F512D64BE5F5}" };
            //timelineStyleExtension.AddNamespaceDeclaration("x15", "http://schemas.microsoft.com/office/spreadsheetml/2010/11/main");
            //timelineStyleExtension.AppendChild(new X15.TimelineStyles() { DefaultTimelineStyle = "TimeSlicerStyleLight1" });

            var stylesheet = new Stylesheet() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "x14ac" } };
            stylesheet.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            stylesheet.AddNamespaceDeclaration("x14ac", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac");

            stylesheet.Append(
                new Fonts(fonts) { Count = (uint)fonts.Count(), KnownFonts = true },
                new Fills(fills) { Count = (uint)fills.Count() },
                new Borders(borders) { Count = (uint)borders.Count() },
                new CellStyleFormats(cellStyleFormats) { Count = (uint)cellStyleFormats.Count() },
                new CellFormats(cellFormats) { Count = (uint)cellFormats.Count() },
                new CellStyles(cellStyles) { Count = (uint)cellStyles.Count() },
                new DifferentialFormats(differentialFormats) { Count = (uint)differentialFormats.Count() },
                new TableStyles(tableStyles) {
                    Count = (uint)tableStyles.Count(),
                    DefaultTableStyle = "TableStyleMedium2",
                    DefaultPivotStyle = "PivotStyleMedium9"
                },
                new StylesheetExtensionList(
                    slicerStyleExtension //,
                                         //timelineStyleExtension
                    )
                );

            var workbookStylesPart = workbookPart.AddNewPart<WorkbookStylesPart>();
            workbookStylesPart.Stylesheet = stylesheet;

            return workbookStylesPart;
        }

        private OpenXmlElement Map(FontStyle style) {
            switch(style) {
                case FontStyle.Bold:
                    return new Bold();

                case FontStyle.Italic:
                    return new Italic();

                case FontStyle.Underline:
                    return new Underline();
                case FontStyle.None:
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(style), style, null);
            }

            return null;
        }
    }
}
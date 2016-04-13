using System;
using System.Collections.Generic;
using System.Linq;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

using ProstoA.Documents.Presentation.Abstractions;

namespace ProstoA.Documents.Presentation.Xlsx.Generators {
    internal sealed class WorksheetPartGenerator {
        public WorksheetPart[] Do(WorkbookPart workbookPart, params Indexed<XlsxWorksheet>[] documentWorksheets) {
            return documentWorksheets.Select(x => Do(workbookPart, x)).ToArray();
        }

        public WorksheetPart Do(WorkbookPart workbookPart, Indexed<XlsxWorksheet> documentWorksheet) {
            var worksheet = new Worksheet { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "x14ac" } };
            worksheet.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            worksheet.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            worksheet.AddNamespaceDeclaration("x14ac", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac");

            var data = MakeData(documentWorksheet.Value).ToArray();
            var rows = data.OfType<Row>().OfType<OpenXmlElement>().ToArray();
            var mergeCells = data.OfType<MergeCell>().OfType<OpenXmlElement>().ToArray();
            var rowBreaks = data.OfType<Break>().OfType<OpenXmlElement>().ToArray();
            var selection = new Selection() {
                ActiveCell = "A1",
                SequenceOfReferences = new ListValue<StringValue>() { InnerText = "A1" }
            };

            worksheet.Append(
                new SheetProperties(new PageSetupProperties() { FitToPage = true }),
                new SheetDimension { Reference = "A1:F6" },
                new SheetViews(new SheetView(selection) {
                    WorkbookViewId = 0U,
                    TabSelected = documentWorksheet.Value.IsActive,
                    ZoomScale = (uint)documentWorksheet.Value.ZoomScale,
                    ZoomScaleNormal = 100U
                }),
                documentWorksheet.Value.RowFormat,
                new Columns(MakeColumns(documentWorksheet.Value)),
                new SheetData(rows),
                mergeCells.Any() ? new MergeCells(mergeCells) { Count = (uint)mergeCells.Count() } : null,
                documentWorksheet.Value.PrintOptions,
                documentWorksheet.Value.PageMargins,
                new PageSetup { PaperSize = 9U, FitToHeight = 0U, Orientation = documentWorksheet.Value.Orientation },
                new RowBreaks(rowBreaks) { Count = (uint)rowBreaks.Count(), ManualBreakCount = (uint)rowBreaks.Count() }
            );

            var worksheetPart = workbookPart.AddNewPart<WorksheetPart>("rId" + documentWorksheet.Index);
            worksheetPart.Worksheet = worksheet;

            return worksheetPart;
        }

        private static IEnumerable<OpenXmlElement> MakeColumns(XlsxWorksheet worksheet) {
            uint i = 0;
            return worksheet.Layout.Columns.Select(column => new Column {
                Min = i + 1,
                Max = i += (uint)column.Repeat,
                Style = column.StyleIndex.HasValue ? new UInt32Value((uint)column.StyleIndex) : null,
                Hidden = column.Hidden ? new BooleanValue(true) : null,
                CustomWidth = column.Size.HasValue ? new BooleanValue(true) : null,
                Width = column.Size,
                //BestFit = http://msdn.microsoft.com/en-us/library/documentformat.openxml.spreadsheet.column.aspx
            });
        }

        private static IEnumerable<OpenXmlElement> MakeData(XlsxWorksheet worksheet) {
            var lastRowIndex = 0;

            foreach(var section in worksheet.Data) {
                var data = section.Data.OrderBy(x => x.Row).Last();
                var rows = worksheet.Layout.Rows.SelectMany(x => Enumerable.Repeat(x, x.Repeat)).ToArray();
                var rc = data.Row - rows.Length;

                if(rc > 0) {
                    rows = rows.Concat(Enumerable.Repeat(DocumentLayoutUnit.Auto, rc)).ToArray();
                }

                var rowsIndo = rows.Select((x, i) => new { Index = i + 1, Props = x });
                var items = rowsIndo.GroupJoin(section.Data, x => x.Index, x => x.Row, (i, o) => new { Row = i, Data = o });

                foreach(var item in items) {
                    var rowIndex = ++lastRowIndex;

                    if(!item.Row.Props.IsDefault && !item.Data.Any()) {
                        continue;
                    }

                    var cells = item.Data
                        .OrderBy(x => x.Column)
                        .Select(x => Enumerable.Repeat(x, x.ColumnSpan))
                        .SelectMany(x => x.Select((p, i) => i == 0
                            ? MakeCell(p, p.Column, rowIndex)
                            : EmptyCell(p, p.Column + i, rowIndex)
                        ));

                    var merges = item.Data
                        .Where(x => x.ColumnSpan > 1)
                        .Select(x => new MergeCell() {
                            Reference = string.Format("{1}{0}:{2}{0}",
                                rowIndex,
                                ColumnA1Reference(x.Column),
                                ColumnA1Reference(x.Column + x.ColumnSpan - 1)
                            )
                        });

                    foreach(var merge in merges) {
                        yield return merge;
                    }

                    var row = new Row(cells) {
                        RowIndex = (uint)rowIndex,
                        StyleIndex = item.Row.Props.StyleIndex.HasValue
                            ? new UInt32Value((uint)item.Row.Props.StyleIndex)
                            : null,
                        //Spans = new ListValue<StringValue>() { InnerText = "1:" + page.GridSystem.Columns.Count() },
                        Hidden = item.Row.Props.Hidden ? new BooleanValue(true) : null,
                        CustomHeight = item.Row.Props.Size.HasValue ? new BooleanValue(true) : null,
                        Height = item.Row.Props.Size,
                        DyDescent = worksheet.RowFormat.DyDescent
                    };

                    yield return row;
                }

                yield return new Break { Id = (uint)++lastRowIndex, ManualPageBreak = true };
            }
        }

        private static string ColumnA1Reference(int columnNumber) {
            var dividend = columnNumber;
            var columnName = string.Empty;

            while(dividend > 0) {
                var modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo) + columnName;
                dividend = (dividend - modulo) / 26;
            }

            return columnName;
        }

        private static Cell MakeCell(XlsxCell cell, int column, int row) {
            if (cell.Data == null) {
                return EmptyCell(cell, column, row);
            }

            return new Cell(new CellValue(cell.Data.Value)) {
                CellReference = ColumnA1Reference(column) + row,
                StyleIndex = (uint)cell.StyleIndex,
                DataType = cell.Data.ValueType
            };
        }

        private static Cell EmptyCell(XlsxCell cell, int column, int row) {
            return new Cell((CellValue)null) {
                CellReference = ColumnA1Reference(column) + row,
                StyleIndex = (uint)cell.StyleIndex,
                DataType = null
            };
        }
    }
}
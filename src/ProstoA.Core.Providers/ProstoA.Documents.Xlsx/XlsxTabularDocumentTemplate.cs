using System;
using System.Collections.Generic;
using System.Linq;

using DocumentFormat.OpenXml.Spreadsheet;

using ProstoA.Data.View;
using ProstoA.Documents.Model;
using ProstoA.Documents.Xlsx.Model;

namespace ProstoA.Documents.Xlsx {
    public class TabularDocumentXlsxTemplate<T> : ITemplate<TabularDocument<T>, FileView> {
        public FileView Apply(TabularDocument<T> document) {
            var sharedStrings = new List<string>();
            var styles = new List<XlsxCellStyle> {
                new XlsxCellStyle(),
                new XlsxCellStyle() { HorizontalAlignment = HorizontalAlignmentValues.Center },
                new XlsxCellStyle(),
            };

            var content = new XlsxFileContent {
                Properties = new XlsxProperties {
                    CreatedBy = document.CreatedBy,
                    Created = document.Created.GetValueOrDefault(DateTimeOffset.Now).LocalDateTime,
                    LastModifiedBy = document.LastModifiedBy,
                    LastModified = document.LastModified.GetValueOrDefault(DateTimeOffset.Now).LocalDateTime
                },
                Worksheets = new[] {
                    new XlsxWorksheet {
                        Title = document.Title,
                        ZoomScale = 100,
                        Layout = new XlsxLayout {
                            Columns = document.Items.Select(x => new DocumentLayoutUnit(x.Size)),
                            Rows = new DocumentLayoutUnit[0]
                        },
                        Data = new[] {
                            new XlsxSection(
                                document.Items.Select((x, i) => new XlsxCell {
                                    Row = 1,
                                    Column = i + 1,
                                    StyleIndex = 1,
                                    Data = GetCellValue(i + 1, sharedStrings),
                                }),
                                document.Items.Select((x, i) => new XlsxCell {
                                    Row = 2,
                                    Column = i + 1,
                                    StyleIndex = 1,
                                    Data = GetCellValue(x.Title, sharedStrings),
                                }),
                                document.Data.SelectMany((item, i) => document.Items.Select((x, j) => new XlsxCell {
                                    Row = i + 3,
                                    Column = j + 1,
                                    StyleIndex = x.ByCenter ? 1 : 2,
                                    Data = GetCellValue(x.GetValue(item, i + 1), sharedStrings)
                                }))
                            )
                        }
                    }
                },
                SharedStrings = sharedStrings.ToArray(),
                Styles = styles.ToArray(),
            };

            return new XlsxFileView(document.Title, content);
        }

        private static XlsxCellValue GetCellValue(object value, IList<string> sharedStrings) {
            if (value == null) {
                return null;
            }

            var v = value.ToString();

            double t;
            var isNumber = double.TryParse(v, out t);

            return isNumber
                ? new XlsxCellValue(v, CellValues.Number)
                : new XlsxCellValue(SharedString(v, sharedStrings), CellValues.SharedString);
        }

        private static string SharedString(string text, IList<string> sharedStrings) {
            var index = sharedStrings.IndexOf(text);
            if(index < 0) {
                sharedStrings.Add(text);
                index = sharedStrings.Count - 1;
            }

            return index.ToString();
        }
    }
}
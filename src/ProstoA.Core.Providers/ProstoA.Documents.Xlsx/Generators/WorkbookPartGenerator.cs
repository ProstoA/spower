using System.Linq;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

using ProstoA.Documents.Xlsx.Model;

namespace ProstoA.Documents.Xlsx.Generators {
    internal sealed class WorkbookPartGenerator {
        public WorkbookPart Do(SpreadsheetDocument package, params XlsxWorksheet[] worksheets) {
            var workbook = new Workbook { MCAttributes = new MarkupCompatibilityAttributes { Ignorable = "x15" } };
            workbook.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            workbook.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            workbook.AddNamespaceDeclaration("x15", "http://schemas.microsoft.com/office/spreadsheetml/2010/11/main");

            workbook.Append(
                new FileVersion { ApplicationName = "xl", LastEdited = "6", LowestEdited = "4", BuildVersion = "14420" },
                new WorkbookProperties { FilterPrivacy = true, DefaultThemeVersion = 124226U },
                new BookViews(
                    new WorkbookView {
                        ActiveTab = (uint)(worksheets.IndexOf(x => x.IsActive) ?? 0),
                        XWindow = 240,
                        YWindow = 105,
                        WindowWidth = 14805U,
                        WindowHeight = 8010U,
                        TabRatio = 845U
                    }
                ),
                new Sheets(worksheets.Select((x, index) => new Sheet {
                    Name = x.Title,
                    SheetId = (uint)index + 1,
                    Id = "rId" + index
                })),
                new CalculationProperties() { CalculationId = 122211U }
            );

            var workbookPart = package.AddWorkbookPart();
            workbookPart.Workbook = workbook;

            return workbookPart;
        }
    }
}
using System.Linq;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ProstoA.Documents.Presentation.Xlsx.Generators {
    internal sealed class WorkbookPartGenerator {
        public WorkbookPart Do(SpreadsheetDocument package, params Indexed<XlsxWorksheet>[] worksheets) {
            var workbook = new Workbook { MCAttributes = new MarkupCompatibilityAttributes { Ignorable = "x15" } };
            workbook.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            workbook.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            workbook.AddNamespaceDeclaration("x15", "http://schemas.microsoft.com/office/spreadsheetml/2010/11/main");

            workbook.Append(
                new FileVersion { ApplicationName = "xl", LastEdited = "6", LowestEdited = "4", BuildVersion = "14420" },
                new WorkbookProperties { FilterPrivacy = true, DefaultThemeVersion = 124226U },
                new BookViews(
                    new WorkbookView {
                        ActiveTab = (uint)worksheets.FirstOrDefault(x => x.Value.IsActive).Index,
                        XWindow = 240,
                        YWindow = 105,
                        WindowWidth = 14805U,
                        WindowHeight = 8010U,
                        TabRatio = 845U
                    }
                ),
                new Sheets(worksheets.Select(x => new Sheet {
                    Name = x.Value.Title,
                    SheetId = (uint)x.Index + 1,
                    Id = "rId" + x.Index
                })),
                new CalculationProperties() { CalculationId = 122211U }
            );

            var workbookPart = package.AddWorkbookPart();
            workbookPart.Workbook = workbook;

            return workbookPart;
        }
    }
}
using System.IO;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;

using ProstoA.Data.Presentation;
using ProstoA.Documents.Presentation.Xlsx.Generators;
using ProstoA.Documents.Presentation.Xlsx.Model;

namespace ProstoA.Documents.Presentation.Xlsx {
    public class XlsxView : FileView {
        public XlsxView(string name, string title = null) {
            Name = name;
            Title = title ?? name;
        }

        public override string Name { get; }

        public override string Title { get; }

        public override string ContentType => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public override string FileExtension => ".xlsx";

        public Indexed<XlsxWorksheet>[] Worksheets { get; set; }

        public Indexed<XlsxCellStyle>[] Styles { get; set; }

        public Indexed<string>[] SharedStrings { get; set; }

        public XlsxProperties Properties { get; set; }

        public override void Write(Stream stream) {
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
}
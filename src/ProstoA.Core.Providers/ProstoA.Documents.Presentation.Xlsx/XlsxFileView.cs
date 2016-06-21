using System.IO;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;

using ProstoA.Data.Presentation;
using ProstoA.Documents.Presentation.Xlsx.Generators;

namespace ProstoA.Documents.Presentation.Xlsx {
    public class XlsxFileView : FileView {
        public XlsxFileView(string name, XlsxFileContent content) {
            Name = name;
            Content = content;
        }

        public override string Name { get; }

        public XlsxFileContent Content { get; }

        public override string ContentType => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public override string FileExtension => ".xlsx";

        protected override void Write(Stream stream) {
            using(var package = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook)) {
                new ExtendedFilePropertiesPartGenerator().Do(package, Content);
                var workbookPart = new WorkbookPartGenerator().Do(package, Content.Worksheets);
                new WorksheetPartGenerator().Do(workbookPart, Content.Worksheets);
                new WorkbookStylesPartGenerator().Do(workbookPart, Content.Styles);
                new SharedStringTablePartGenerator().Do(workbookPart, Content.SharedStrings);

                package.PackageProperties.Creator = Content.Properties.CreatedBy;
                package.PackageProperties.Created = Content.Properties.Created;
                package.PackageProperties.Modified = Content.Properties.LastModified;
                package.PackageProperties.LastModifiedBy = Content.Properties.LastModifiedBy;
            }
        }
    }
}
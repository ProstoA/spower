using System.Linq;

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ProstoA.Documents.Xlsx.Generators {
    internal sealed class SharedStringTablePartGenerator {
        public SharedStringTablePart Do(WorkbookPart workbookPart, params string[] strings) {
            var sharedStringTable = new SharedStringTable() {
                Count = (uint)strings.Length,
                UniqueCount = (uint)strings.Length
            };

            sharedStringTable.Append(
                strings.Select(x => new SharedStringItem(new Text { Text = x }))
            );

            var sharedStringTablePart = workbookPart.AddNewPart<SharedStringTablePart>();
            sharedStringTablePart.SharedStringTable = sharedStringTable;

            return sharedStringTablePart;
        }
    }
}
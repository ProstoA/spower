using System.Collections.Generic;
using System.Linq;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.VariantTypes;

namespace ProstoA.Documents.Xlsx.Generators {
    internal sealed class ExtendedFilePropertiesPartGenerator {
        public ExtendedFilePropertiesPart Do(SpreadsheetDocument package, XlsxFileContent content) {
            var properties = new Properties();

            properties.AddNamespaceDeclaration("vt", "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes");
            properties.Append(
                new Application { Text = "Microsoft Excel" },
                new DocumentSecurity { Text = "0" },
                new ScaleCrop { Text = "false" },
                new HeadingPairs(
                    MakeVector(VectorBaseValues.Variant,
                        new Variant(new VTLPSTR { Text = "Листы" }),
                        new Variant(new VTInt32 { Text = content.Worksheets.Length.ToString() })
                        )
                    ),
                new TitlesOfParts(
                    MakeVector(VectorBaseValues.Lpstr,
                        content.Worksheets.Select(x => new VTLPSTR { Text = x.Title })
                        )
                    ),
                new Company { Text = content.Properties.Company },
                new LinksUpToDate { Text = "false" },
                new SharedDocument { Text = "false" },
                new HyperlinksChanged { Text = "false" },
                new ApplicationVersion { Text = "15.0300" }
                );

            var extendedFilePropertiesPart = package.AddNewPart<ExtendedFilePropertiesPart>();
            extendedFilePropertiesPart.Properties = properties;

            return extendedFilePropertiesPart;
        }

        private static VTVector MakeVector<T>(VectorBaseValues type, IEnumerable<T> values) where T : OpenXmlElement {
            return MakeVector(type, values.OfType<OpenXmlElement>().ToArray());
        }

        private static VTVector MakeVector(VectorBaseValues type, params OpenXmlElement[] values) {
            return new VTVector(values) { BaseType = type, Size = (uint)values.Length };
        }
    }
}
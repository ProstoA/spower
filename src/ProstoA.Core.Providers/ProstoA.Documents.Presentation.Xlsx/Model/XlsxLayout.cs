using System;
using System.Collections.Generic;

namespace ProstoA.Documents.Presentation.Xlsx.Model {
    public class XlsxLayout  {
        public IEnumerable<DocumentLayoutUnit> Columns { get; set; }

        public IEnumerable<DocumentLayoutUnit> Rows { get; set; }
    }

    public struct DocumentLayoutUnit {
        public static readonly DocumentLayoutUnit Auto = new DocumentLayoutUnit(null);

        public DocumentLayoutUnit(float? size, int repeat = 1, bool hidden = false, int? style = null) {
            if(repeat < 1) {
                throw new ArgumentException("Значение должно быть больше 1", "repeat");
            }

            Size = size;
            Repeat = repeat;
            Hidden = hidden;
            StyleIndex = style;
        }

        public double? Size { get; }

        public int Repeat { get; }

        public bool Hidden { get; }

        public int? StyleIndex { get; }

        public bool IsDefault => !Size.HasValue && !Hidden && StyleIndex.HasValue;
    }
}
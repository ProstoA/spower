using System;
using System.Collections.Generic;

using ProstoA.Documents.Model;
using ProstoA.Documents.Presentation.Abstractions;

namespace ProstoA.Documents.Presentation.Xlsx {
    public class ListDocument<T> : IDocument {
        public ListDocument(string name, string title, IEnumerable<T> data) {
            Name = name;
            Title = title;
            Data = data;
        }

        public string Name { get; }

        public string Title { get; }

        public string CreatedBy { get; set; }

        public DateTimeOffset? Created { get; set; }

        public string ModifiedBy { get; set; }

        public DateTimeOffset? Modified { get; set; }

        public IEnumerable<T> Data { get; }
    }

    public class ListDocumentColum : IFormItem {
        public static ListDocumentColum Left<T>(IEnumerable<T> items, string title, int size, Func<Value<T>, object> getValie) {
            var column = Make(items, title, size, getValie);
            column.ByCenter = false;
            return column;
        }

        public static ListDocumentColum Center<T>(IEnumerable<T> items, string title, int size, Func<Value<T>, object> getValie) {
            var column = Make(items, title, size, getValie);
            column.ByCenter = true;
            return column;
        }

        private static ListDocumentColum Make<T>(IEnumerable<T> items, string title, int size, Func<Value<T>, object> getValie) {
            return new ListDocumentColum {
                Title = title,
                Size = size,
                ByCenter = true,
                _getValue = (x, i) => getValie(new Value<T>(x, i))
            };
        }

        public int Size { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public bool Selected { get; set; }

        public bool Disabled { get; set; }

        public bool Hidden { get; set; }

        public IDocumentLayout Layout { get; set; }

        public IEnumerable<IFormItem> Items { get; set; }

        public bool ByCenter { get; set; }

        private Func<object, int, object> _getValue;

        public object GetValue(object row, int index) {
            return _getValue(row, index);
        }

        public class Value<T> {
            public Value(object row, int index) {
                Index = index;
                Row = (T)row;
            }

            public int Index { get; private set; }

            public T Row { get; private set; }
        }
    }
}
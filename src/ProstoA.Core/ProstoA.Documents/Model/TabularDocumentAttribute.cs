using System;
using System.Collections.Generic;

using ProstoA.Data.Metamodel;
using ProstoA.Data.Model;


namespace ProstoA.Documents.Model {
    public class TabularDocumentFields : IFormItem {
        public static TabularDocumentFields Left<T>(IEnumerable<T> items, string title, int size, Func<Value<T>, object> getValie) {
            var column = Make(items, title, size, getValie);
            column.ByCenter = false;
            return column;
        }

        public static TabularDocumentFields Center<T>(IEnumerable<T> items, string title, int size, Func<Value<T>, object> getValie) {
            var column = Make(items, title, size, getValie);
            column.ByCenter = true;
            return column;
        }

        private static TabularDocumentFields Make<T>(IEnumerable<T> items, string title, int size, Func<Value<T>, object> getValie) {
            return new TabularDocumentFields {
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
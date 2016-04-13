using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProstoA {
    public interface ISubset<out T> : IReadOnlyCollection<T> {
        int Offset { get; }

        int Total { get; }
    }

    public class Subset<T> : ISubset<T> {
        private readonly IEnumerable<T> _items;

        public Subset(IEnumerable<T> items, int offset, int total) {
            _items = items;
            Offset = offset;
            Total = total;
        }

        public int Count => _items.Count();

        public IEnumerator<T> GetEnumerator() {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public int Offset { get; }

        public int Total { get; }
    }
}
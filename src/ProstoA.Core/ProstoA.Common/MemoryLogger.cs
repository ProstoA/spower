using System.Collections.Generic;

namespace ProstoA {
    public class MemoryLogger : ILogger {
        private readonly List<string> _log = new List<string>();

        public void WriteLog(string text) {
            _log.Add(text);
        }

        public IReadOnlyCollection<string> Log => _log.AsReadOnly();
    }
}
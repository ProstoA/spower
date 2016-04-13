using System.Collections.Generic;

namespace ProstoA {
    public interface ILogger {
        void WriteLog(string text);

        IReadOnlyCollection<string> Log { get; }
    }
}
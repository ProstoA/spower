using System;

using Microsoft.SharePoint.Utilities;

namespace ProstoA.Spower {
    public static class Monitoring {
#if DEBUG
        internal class EmptyDisposable : IDisposable {
            public void Dispose() { }
        }

        private static readonly EmptyDisposable Empty = new EmptyDisposable();
#endif

        public static IDisposable StartHighScope(string name) {
#if DEBUG
            return Empty;
#else
            return new SPMonitoredScope(name, TraceSeverity.High);
#endif
        }

        public static IDisposable StartScope(string name, params ISPScopedPerformanceMonitor[] monitors) {
#if DEBUG
            return Empty;
#else
            return new SPMonitoredScope(name, TraceSeverity.Verbose, monitors);
#endif
        }

        public static IDisposable StartScopeWithTimeLimit(string name, uint milliseconds, params ISPScopedPerformanceMonitor[] monitors) {
#if DEBUG
            return Empty;
#else
            return new SPMonitoredScope(name, milliseconds, monitors);
#endif
        }
    }
}
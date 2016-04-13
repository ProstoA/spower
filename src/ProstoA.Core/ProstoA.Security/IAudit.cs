using System;

namespace ProstoA.Security {
    public interface IAudit<out TSubject> where TSubject : IAuditSubject {
        DateTimeOffset Created { get; }

        TSubject Author { get; }

        DateTimeOffset Modified { get; }

        TSubject Editor { get; }
    }
}
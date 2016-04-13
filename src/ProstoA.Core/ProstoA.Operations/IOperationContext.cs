using System;
using ProstoA.Data.Object.Abstractions;
using ProstoA.Security;

namespace ProstoA.Operations {
    public interface IOperationContext {
        IObjectIdentity<IUserInfo> UserIdentity { get; }

        DateTimeOffset OperationDate { get; }
    }
}
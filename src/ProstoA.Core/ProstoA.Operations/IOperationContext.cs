using System;

using ProstoA.Data.Metamodel;
using ProstoA.Security;

namespace ProstoA.Operations {
    public interface IOperationContext {
        IObjectIdentity<IUserInfo> UserIdentity { get; }

        DateTimeOffset OperationDate { get; }
    }
}
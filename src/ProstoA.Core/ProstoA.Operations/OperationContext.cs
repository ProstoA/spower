using System;

using ProstoA.Data.Metamodel;
using ProstoA.Security;

namespace ProstoA.Operations {
    public class OperationContext : IOperationContext {
        public OperationContext(IObjectIdentity<IUserInfo> userIdentity, DateTimeOffset? operationDate = null) {
            UserIdentity = userIdentity;
            OperationDate = operationDate ?? DateTimeOffset.Now;
        }

        public IObjectIdentity<IUserInfo> UserIdentity { get; }

        public DateTimeOffset OperationDate { get; }
    }
}
using ProstoA.Data.Metamodel;

namespace ProstoA.Security {
    public interface IUserInfo : IObjectIdentity<IUserInfo>, IAuditSubject { }
}
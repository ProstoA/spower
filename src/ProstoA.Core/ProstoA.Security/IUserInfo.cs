using ProstoA.Data.Object.Abstractions;

namespace ProstoA.Security {
    public interface IUserInfo : IObjectIdentity<IUserInfo>, IAuditSubject { }
}
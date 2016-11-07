using System.Web;

namespace ProstoA.Spower {
    public interface ISpowerHandlerFactory : IHttpHandlerFactory {
        string[] AllowExts { get; }
    }
}
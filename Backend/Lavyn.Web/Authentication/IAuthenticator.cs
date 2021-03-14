using System.Threading.Tasks;

namespace Lavyn.Web.Authentication
{
    public interface IAuthenticator<T> where T : class
    {
        string Login(T identity);
    }
}

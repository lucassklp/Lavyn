using System.Threading.Tasks;

namespace Lavyn.Web.Authentication
{
    public interface IAuthenticator<T> where T : class
    {
        Task<string> Login(T identity);
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lavyn.Domain;
using Lavyn.Domain.Dtos;
using Lavyn.Web.Authentication;

namespace Lavyn.Web.Controllers
{
    [Route("api/login")]
    public class LoginController : Controller
    {
        IAuthenticator<ICredential> authenticator;
        public LoginController(IAuthenticator<ICredential> authenticator)
        {
            this.authenticator = authenticator;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] CredentialDto user)
        {
            string token = authenticator.Login(user);
            if(string.IsNullOrWhiteSpace(token))
            {
                return Unauthorized();
            }
            return Json(new { token });
        }
    }
}

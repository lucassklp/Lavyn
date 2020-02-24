using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Lavyn.Business;
using Lavyn.Domain.Entities;

namespace Lavyn.Controllers
{
    [AllowAnonymous]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private UserServices userServices;

        public AccountController(UserServices userServices)
        {
            this.userServices = userServices;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            return Ok(await userServices.RegisterAsync(user));
        }
    }
}

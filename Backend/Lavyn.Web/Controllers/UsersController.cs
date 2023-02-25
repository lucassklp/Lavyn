using Lavyn.Business;
using Microsoft.AspNetCore.Mvc;

namespace Lavyn.Web.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        private UserServices userServices;
        public UsersController(UserServices userServices)
        {
            this.userServices = userServices;
        }
    }
}
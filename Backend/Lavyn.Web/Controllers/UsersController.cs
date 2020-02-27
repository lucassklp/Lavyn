using System.Collections.Generic;
using System.Threading.Tasks;
using Lavyn.Business;
using Lavyn.Domain.Entities;
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
        
        [HttpGet]
        [Route("online")]
        public IActionResult GetOnlineUsers()
        {
            return Ok(userServices.GetOnlineUsers());
        }
    }
}
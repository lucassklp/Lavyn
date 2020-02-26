using System.Collections.Generic;
using System.Threading.Tasks;
using Lavyn.Business;
using Lavyn.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Lavyn.Web.Controllers
{
    [Route("api/room")]
    public class RoomController : Controller
    {
        private RoomServices roomServices;
        public RoomController(RoomServices roomServices)
        {
            this.roomServices = roomServices;
        }
        
        [HttpGet]
        [Route("with-user/{id}")]
        public async Task<IActionResult> GetOnlineUsers(long id)
        {
            return Ok(roomServices.GetChatId(id));
        }
    }
}
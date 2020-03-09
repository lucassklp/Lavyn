using System.Collections.Generic;
using System.Threading.Tasks;
using Lavyn.Business;
using Lavyn.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Lavyn.Web.Controllers
{
    [Route("api/chat")]
    public class ChatController : Controller
    {
        private RoomServices roomServices;
        public ChatController(RoomServices roomServices)
        {
            this.roomServices = roomServices;
        }
        
        [HttpGet]
        [Route("with-user/{id}")]
        public async Task<IActionResult> GetOnlineUsers(long id)
        {
            return Ok(await roomServices.GetOrCreateRoomWith(id));
        }
    }
}
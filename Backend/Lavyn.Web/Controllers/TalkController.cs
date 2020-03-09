using System.Threading.Tasks;
using Lavyn.Business;
using Microsoft.AspNetCore.Mvc;

namespace Lavyn.Web.Controllers
{
    [Route("api/talk")]
    public class TalkController : Controller
    {
        private RoomServices roomServices;
        public TalkController(RoomServices roomServices)
        {
            this.roomServices = roomServices;
        }
        
        [HttpGet]
        [Route("with-user/{id}")]
        public async Task<IActionResult> GetTalkWith(long id)
        {
            return Ok(await roomServices.GetOrCreateRoomWith(id));
        }
    }
}
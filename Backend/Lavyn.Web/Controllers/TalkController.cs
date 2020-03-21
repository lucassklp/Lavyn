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
            return Ok(await roomServices.GetOrCreateRoom(null, id));
        }

        [HttpPost]
        [Route("create-group/{name}")]
        public async Task<IActionResult> GetOrCreateRoom([FromBody] long[] participantIds, string name)
        {
            return Ok(await roomServices.GetOrCreateRoom(name, participantIds));
        }
        
        [HttpGet]
        [Route("my-talks")]
        public IActionResult GetMyTalks()
        {
            return Ok(roomServices.GetRoomsAuthenticatedUser());
        }
    }
}
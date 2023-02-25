

using System;
using System.Threading.Tasks;
using Lavyn.Business;
using Lavyn.Domain.Dtos;
using Lavyn.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Lavyn.Web.Hubs
{
    [Authorize]
    public class CallHub : Hub
    {
        private CallServices _callServices;
        private User _authenticatedUser;
        public CallHub(CallServices callServices, User authenticatedUser)
        {
            _callServices = callServices;
            _authenticatedUser = authenticatedUser;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var roomAndPeer = _callServices.Disconnect();
            Clients.Group(roomAndPeer.RoomKey + "_call")
                .SendAsync("leave-call", roomAndPeer.PeerId);
            return base.OnDisconnectedAsync(exception);
        }

        [HubMethodName("enter-call")]
        public async Task EnterCall(EnterCallDto enterCall)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, enterCall.Key + "_call");
            await Clients.Caller.SendAsync("enter-call", _callServices.Connect(enterCall));
        }
    }
}
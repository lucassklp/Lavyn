using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Lavyn.Business;
using Lavyn.Domain.Dtos;
using Lavyn.Domain.Entities;
using Lavyn.Domain.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Lavyn.Web.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private ChatServices _chatServices;
        private RoomServices _roomServices;
        private User _authenticatedUser;
        public ChatHub(ChatServices chatServices, RoomServices roomServices, User authenticatedUser)
        {
            _chatServices = chatServices;
            _roomServices = roomServices;
            _authenticatedUser = authenticatedUser;
        }
        
        public override async Task OnConnectedAsync()
        {
            var user = await _chatServices.SetAuthenticatedUserOnline(true);
            var rooms = _roomServices.GetRoomsAuthenticatedUser();

            foreach (var room in rooms)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, room.Key);
                await Clients.OthersInGroup(room.Key).SendAsync("enter-room", new UserInRoomDto
                {
                    UserId = user.Id,
                    RoomKey = room.Key
                });
            }
            
            await Clients.Caller.SendAsync("my-rooms", rooms);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = await _chatServices.SetAuthenticatedUserOnline(false);
            var rooms = _roomServices.GetRoomsAuthenticatedUser();

            foreach (var room in rooms)
            {
                await Clients.OthersInGroup(room.Key).SendAsync("leave-room", new UserInRoomDto
                {
                    UserId = user.Id,
                    RoomKey = room.Key
                });
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, room.Key);
            }
            
            await base.OnDisconnectedAsync(exception);
        }

        [HubMethodName("viewed-room")]
        public async Task ViewedMessages(string roomKey)
        {
            var userHasRoom = await _roomServices.SetLastViewedRoom(roomKey);
            await Clients.Group(roomKey).SendAsync("viewed-room", new ViewedMessageDto()
            {
                LastSeen = userHasRoom.LastSeen,
                RoomKey = roomKey,
                UserId = userHasRoom.UserId
            });
        }

        [HubMethodName("send-message")]
        public async Task SendMessage(ChatMessageDto chat)
        {
            _chatServices.SendMessageAsync(chat).Subscribe();
            await Clients.Group(chat.RoomKey)
                .SendAsync("received-message", new RoomMessageDto
                {
                    Date = DateTime.Now,
                    Message = chat.Message,
                    SenderId = _authenticatedUser.Id,
                    RoomKey = chat.RoomKey
                });
            await ViewedMessages(chat.RoomKey);
        }

        [HubMethodName("connect-call")]
        public async Task Call(CallDto call)
        {
            var token = _chatServices.CreateCallToken(call);
            await Clients.Group(call.Key)
                .SendAsync("call", new CallDto()
                {
                    CallType = call.CallType,
                    Key = token,
                    CallerId = _authenticatedUser.Id
                });
        }
    }
}
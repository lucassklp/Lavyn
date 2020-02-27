using System;
using System.Threading.Tasks;
using Auto.Mapping.DependencyInjection;
using Lavyn.Business.Mapping;
using Lavyn.Domain.Dtos;
using Lavyn.Domain.Entities;
using Lavyn.Persistence.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Lavyn.Web.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private ILogger<ChatHub> _logger;
        private User _authenticatedUser;
        private IMapResolver _mapResolver;
        private UserRepository _userRepository;
        private RoomRepository _roomRepository;
        private MessageRepository _messageRepository;
        public ChatHub(
            ILogger<ChatHub> logger,
            User authenticatedUser,
            IMapResolver mapResolver,
            UserRepository userRepository,
            RoomRepository roomRepository,
            MessageRepository messageRepository)
        {
            _logger = logger;
            _authenticatedUser = authenticatedUser;
            _mapResolver = mapResolver;
            _userRepository = userRepository;
            _roomRepository = roomRepository;
            _messageRepository = messageRepository;
        }
        
        public override Task OnConnectedAsync()
        {
            _logger.LogInformation($"{_authenticatedUser.Name} has been connected");
            
            _authenticatedUser.IsOnline = true;
            _userRepository.Update(_authenticatedUser);
            
            var rooms = _roomRepository.GetGroupsIdByUser(_authenticatedUser.Id);

            foreach (var room in rooms)
            {
                Groups.AddToGroupAsync(Context.ConnectionId, room);
            }

            Clients.Others.SendAsync("enter-room", new UserToUserDtoMapping().Map(_authenticatedUser));
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _logger.LogInformation($"{_authenticatedUser.Name} has been disconnected");

            _authenticatedUser.IsOnline = false;
            _userRepository.UpdateAsync(_authenticatedUser).Subscribe();

            var rooms = _roomRepository.GetGroupsIdByUser(_authenticatedUser.Id);

            foreach (var room in rooms)
            {
                Groups.RemoveFromGroupAsync(Context.ConnectionId, room);
            }

            Clients.Others.SendAsync("leave-room", new UserToUserDtoMapping().Map(_authenticatedUser));
            return base.OnDisconnectedAsync(exception);
        }

        [HubMethodName("send-message")]
        public Task SendMessage(ChatMessageDto chat)
        {
            var message = new Message()
            {
                SenderId = _authenticatedUser.Id,
                RoomId = chat.RoomId,
                Date = DateTime.Now,
                Content = chat.Message
            };

            _messageRepository.CreateAsync(message).Subscribe();

            return Clients.OthersInGroup(chat.RoomId.ToString())
                .SendAsync("received-message", chat);
        }
    }
}
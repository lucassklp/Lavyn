using System;
using System.Threading.Tasks;
using Auto.Mapping.DependencyInjection;
using Lavyn.Business.Mapping;
using Lavyn.Domain.Dtos;
using Lavyn.Domain.Entities;
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
        public ChatHub(ILogger<ChatHub> logger, User authenticatedUser, IMapResolver mapResolver)
        {
            _logger = logger;
            _authenticatedUser = authenticatedUser;
            _mapResolver = mapResolver;
        }
        
        public override Task OnConnectedAsync()
        {
            _logger.LogInformation($"{_authenticatedUser.Name} has been connected");
            Clients.Others.SendAsync("enter-room", new UserToUserDtoMapping().Map(_authenticatedUser));
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _logger.LogInformation($"{_authenticatedUser.Name} has been disconnected");
            Clients.Others.SendAsync("leave-room", _mapResolver.Map<User, UserDto>(_authenticatedUser));
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(ChatMessageDto chat)
        {
             await Clients.Others.SendAsync("ReceiveMsg", chat);
        }
    }
}
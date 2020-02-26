using System;
using System.Threading.Tasks;
using Auto.Mapping.DependencyInjection;
using Lavyn.Business;
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

        private Crud<long, User> _crudUser;
        public ChatHub(
            ILogger<ChatHub> logger,
            User authenticatedUser,
            IMapResolver mapResolver,
            Crud<long, User> crudUser)
        {
            _logger = logger;
            _authenticatedUser = authenticatedUser;
            _mapResolver = mapResolver;
            _crudUser = crudUser;

        }
        
        public override Task OnConnectedAsync()
        {
            _logger.LogInformation($"{_authenticatedUser.Name} has been connected");
            
            _authenticatedUser.IsOnline = true;
            _crudUser.UpdateAsync(_authenticatedUser).Subscribe();
            
            Clients.Others.SendAsync("enter-room", new UserToUserDtoMapping().Map(_authenticatedUser));
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _logger.LogInformation($"{_authenticatedUser.Name} has been disconnected");

            _authenticatedUser.IsOnline = false;
            _crudUser.UpdateAsync(_authenticatedUser).Subscribe();

            Clients.Others.SendAsync("leave-room", new UserToUserDtoMapping().Map(_authenticatedUser));
            return base.OnDisconnectedAsync(exception);
        }

        [HubMethodName("send-message")]
        public async Task SendMessage(ChatMessageDto chat)
        {
             await Clients.Others.SendAsync("ReceiveMsg", chat);
        }
    }
}
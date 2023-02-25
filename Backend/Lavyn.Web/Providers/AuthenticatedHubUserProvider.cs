using System;
using Lavyn.Domain.Entities;
using Lavyn.Persistence.Repository;
using Microsoft.AspNetCore.SignalR;

namespace Lavyn.Web.Providers
{
    public class AuthenticatedHubUserProvider
    {
        private Crud<long, User> _userCrud;
        public AuthenticatedHubUserProvider(Crud<long, User> userCrud)
        {
            _userCrud = userCrud;
        }
        
        public User GetUserId(HubConnectionContext connection)
        {
            var id = connection.User?.Identity?.Name;
            return _userCrud.Read(Convert.ToInt64(id));
        }
    }
}
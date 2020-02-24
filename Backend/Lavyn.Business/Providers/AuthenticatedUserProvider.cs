using System;
using System.Linq;
using System.Security.Claims;
using Lavyn.Domain.Entities;
using Lavyn.Persistence.Repository;
using Microsoft.AspNetCore.Http;

namespace Lavyn.Business.Providers
{
    public class AuthenticatedUserProvider
    {
        private readonly Crud<long, User> _userCrud;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthenticatedUserProvider(IHttpContextAccessor httpContextAccessor, Crud<long, User> userCrud)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._userCrud = userCrud;
        }

        public User GetAuthenticatedUser()
        {
            var id = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return id != null ? _userCrud.Read(Convert.ToInt64(id)) : null;
        }
    }
}


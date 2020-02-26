using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lavyn.Core;
using Lavyn.Domain;
using Lavyn.Domain.Entities;
using Lavyn.Domain.Entities.Enums;

namespace Lavyn.Persistence.Repository
{
    public class UserRepository : AbstractRepository<User>
    {
        public UserRepository(DbContext daoContext) : base(daoContext)
        {
        }

        public bool IsRegistred(User user)
        {
            return Query().Any(x => x.Email == user.Email);
        }

        public IObservable<bool> IsRegistredAsync(User user)
        {
            return SingleObservable.Create(() => IsRegistred(user));
        }

        public User Login(ICredential credential)
        {
            return Query()
                .Include(u => u.UserRoles)
                    .ThenInclude(userRoles => userRoles.Role)
                .Single(x => x.Email.Equals(credential.Login) && x.Password.Equals(credential.Password));
        }

        public List<User> GetUsersByRoom(long roomId) => Query<UserHasRoom>()
            .Where(x => x.RoomId == roomId)
            .Select(x => x.User)
            .ToList();

        public Token GetToken(long userId, TokenType type)
        {
            return Query<Token>().Where(x => x.UserId == userId && x.Type == type).FirstOrDefault();
        }

        public IObservable<User> LoginAsync(ICredential credential)
        {
            return SingleObservable.Create(() => Login(credential));
        }

        public async Task<List<User>> GetOnlineUsers()
        {
            return await Query().Where(x => x.IsOnline)
                .OrderByDescending(x => x.LastLogin)
                .Select(x => new User()
                {
                    Id = x.Id,
                    LastLogin = x.LastLogin,
                    Name = x.Name,
                    IsOnline = x.IsOnline
                })
                .ToListAsync();
        }
    }
}

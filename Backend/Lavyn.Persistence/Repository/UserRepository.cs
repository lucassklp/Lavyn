﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lavyn.Core;
using Lavyn.Domain;
using Lavyn.Domain.Entities;

namespace Lavyn.Persistence.Repository
{
    public class UserRepository : AbstractRepository<User>
    {
        public UserRepository(DbContext daoContext) : base(daoContext)
        {
        }

        public bool IsRegistred(User user)
        {
            return Set().Any(x => x.Email == user.Email);
        }

        public IObservable<bool> IsRegistredAsync(User user)
        {
            return SingleObservable.Create(() => IsRegistred(user));
        }

        public User Login(ICredential credential)
        {
            return Set()
                .Include(u => u.UserRoles)
                    .ThenInclude(userRoles => userRoles.Role)
                .Single(x => x.Email.Equals(credential.Login) && x.Password.Equals(credential.Password));
        }


        public IObservable<User> LoginAsync(ICredential credential)
        {
            return SingleObservable.Create(() => Login(credential));
        }

        public async Task<List<User>> GetOnlineUsers()
        {
            return await Set().Where(x => x.IsOnline)
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

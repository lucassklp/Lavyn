using Lavyn.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Auto.Mapping.DependencyInjection;
using Lavyn.Business.Exceptions;
using Lavyn.Domain.Dtos;
using Lavyn.Domain.Entities;
using Lavyn.Persistence.Repository;

namespace Lavyn.Business
{
    public class UserServices
    {
        UserRepository userRepository;
        Crud<long, User> crud;
        private IMapResolver mapResolver;
        public UserServices(UserRepository userRepository, Crud<long, User> crud, IMapResolver mapResolver)
        {
            this.userRepository = userRepository;
            this.crud = crud;
            this.mapResolver = mapResolver;
        }

        public async Task<List<UserDto>> GetOnlineUsers()
        {
            return (await userRepository.GetOnlineUsers()).Select(x => mapResolver.Map<User, UserDto>(x)).ToList();
        }
        
        
        
        public IObservable<User> RegisterAsync(User user)
        {
            return userRepository.IsRegistredAsync(user).Select(isRegistred =>
            {
                if (!isRegistred)
                {
                    user.Password = user.Password.ToSha512();
                    return this.crud.Create(user);
                }
                else
                {
                    throw new ExistentUserException(user);
                }
            });
        }
    }
}

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
using Lavyn.Business.Mapping;

namespace Lavyn.Business
{
    public class UserServices : AbstractServices<User>
    {
        private UserRepository userRepository;
        private IMapResolver mapResolver;
        public UserServices(UserRepository userRepository, IMapResolver mapResolver)
            : base(userRepository)
        {
            this.userRepository = userRepository;
            this.mapResolver = mapResolver;
        }

        public List<UserDto> GetOnlineUsers()
        {
            return userRepository.GetOnlineUsers()
                .Select(x => new UserToUserDtoMapping().Map(x))
                .ToList();
        }
        
        public IObservable<User> RegisterAsync(User user)
        {
            return userRepository.IsRegistredAsync(user).Select(isRegistred =>
            {
                if (!isRegistred)
                {
                    user.Password = user.Password.ToSha512();
                    return this.Repository.Create(user);
                }
                else
                {
                    throw new ExistentUserException(user);
                }
            });
        }
    }
}

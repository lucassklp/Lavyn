using Lavyn.Core.Extensions;
using System;
using System.Linq;
using System.Reactive.Linq;
using Auto.Mapping.DependencyInjection;
using Lavyn.Business.Exceptions;
using Lavyn.Domain.Entities;
using Lavyn.Persistence.Repository;

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

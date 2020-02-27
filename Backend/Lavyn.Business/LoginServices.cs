using Lavyn.Core.Extensions;
using System;
using System.Threading.Tasks;
using Lavyn.Business.Exceptions;
using Lavyn.Domain;
using Lavyn.Domain.Entities;
using Lavyn.Persistence.Repository;

namespace Lavyn.Business
{
    public class LoginServices
    {
        private UserRepository repository;
        public LoginServices(UserRepository repository)
        {
            this.repository = repository;
        }

        public User Login(ICredential credential)
        {
            credential.Password = credential.Password.ToSha512();
            try
            {
                var user = repository.Login(credential);
                user.LastLogin = DateTime.Now;
                repository.UpdateAsync(user).Subscribe();
                return user;
            }
            catch(InvalidOperationException ex)
            {
                throw new InvalidCredentialException(ex);
            }
        }

        public IObservable<User> SetOnline(User user, bool isOnline)
        {
            user.IsOnline = isOnline;
            return this.repository.UpdateAsync(user);
        }

        public async Task<User> LoginAsync(ICredential credential) => Login(credential);
    }
}

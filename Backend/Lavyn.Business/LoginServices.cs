﻿using Lavyn.Core.Extensions;
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
                return repository.Login(credential);
            }
            catch(InvalidOperationException ex)
            {
                throw new InvalidCredentialException(ex);
            }
        }

        public async Task<User> LoginAsync(ICredential credential) => Login(credential);
    }
}

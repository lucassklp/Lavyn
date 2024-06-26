﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Lavyn.Business;
using Lavyn.Domain;
using Lavyn.Domain.Entities;

namespace Lavyn.Web.Authentication.Jwt
{
    public class JwtAuthenticator : IAuthenticator<ICredential>
    {
        private LoginServices loginService;
        public JwtAuthenticator(LoginServices repository)
        {
            loginService = repository;
        }

        public string Login(ICredential credential)
        {
            var user = loginService.Login(credential);

            if (user != null)
            {
                return GenerateToken(user);
            }
            else return null;
        }

        private string GenerateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.GivenName, user.Name));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            foreach (var userRole in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Description));
            }

            ClaimsIdentity identity = new ClaimsIdentity(claims);

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = JwtTokenDefinitions.Issuer,
                Audience = JwtTokenDefinitions.Audience,
                SigningCredentials = JwtTokenDefinitions.SigningCredentials,
                Subject = identity,
                Expires = DateTime.Now.Add(JwtTokenDefinitions.TokenExpirationTime),
                NotBefore = DateTime.Now
            });

            return handler.WriteToken(securityToken);
        }
    }
}

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using Flashcards.Domain.IRepositories;
using Flashcards.Security.Helpers;
using Flashcards.Security.IServices;
using Flashcards.Security.Models;
using Flashcards_backend.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Flashcards.Security.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IAuthenticationHelper _authenticationHelper;
        private readonly IUserRepository _repo;

        public SecurityService(IConfiguration configuration,
            IUserRepository repository, IAuthenticationHelper authenticationHelper)
        {
            Configuration = configuration;
            _repo = repository;
            _authenticationHelper = authenticationHelper;
        }

        public IConfiguration Configuration { get; }

        public JwtToken GenerateJwtToken(string email, string password)
        {
            var user = _repo.GetAll().FirstOrDefault(user => user.Email.Equals(email));
            if (user == null)
                return new JwtToken
                {
                    Message = "User or password not correct"
                };


            //Did we not find a user with the given username?
            if (_authenticationHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)
            )
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    Configuration["Jwt:Secret"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(Configuration["Jwt:Issuer"],
                    Configuration["Jwt:Audience"],
                    null,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                );
                return new JwtToken
                {
                    Jwt = new JwtSecurityTokenHandler().WriteToken(token),
                    Message = "ok"
                };
            }

            //dont need else
            return new JwtToken
            {
                Message = "User or password not correct"
            };
        }

        public bool Create(string loginDtoEmail, string loginDtoPassword)
        {
            _authenticationHelper.CreatePasswordHash(loginDtoPassword,
                out var hash, out var salt);
            return _repo.Create(new User
            {
                Email = loginDtoEmail,
                PasswordHash = hash,
                PasswordSalt = salt
            });
        }

        public bool EmailExists(string email)
        {
            var user = _repo.GetAll().FirstOrDefault(user => user.Email.Equals(email));
            return user != null;
        }
    }
}
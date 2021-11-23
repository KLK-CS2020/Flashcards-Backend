using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Flashcards.Security.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Flashcards.Security
{
    public class SecurityService: ISecurityService
    {
        public SecurityService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public JwtToken generateJwtToken(string email, string password)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                Configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(Configuration["Jwt:Issuer"],
                Configuration["Jwt:Audience"],
                null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );
            return new JwtToken(
            )
            {
                Jwt = new JwtSecurityTokenHandler().WriteToken(token),
                Message = "ok"
            };
        }
    }
}
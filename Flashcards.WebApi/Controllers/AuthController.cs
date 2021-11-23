using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flashcards.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flashcards.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISecurityService _securityService;

        public AuthController(ISecurityService securityService)
        {
            _securityService = securityService;
        }
       

        // POST: api/Login
        [AllowAnonymous] //people cant log in not being logged in
        [HttpPost(nameof(Login))]
        public ActionResult<TokenDto> Login([FromBody] LoginDto loginDto)
        {
            var token = _securityService.GenerateJwtToken(loginDto.Email, loginDto.Password);
            return new TokenDto
            {
                Jwt = token.Jwt,
                Message = token.Message
            };
            
        }
        
        [AllowAnonymous]
        [HttpPost(nameof(Register))]
        public ActionResult<Boolean> Register([FromBody] LoginDto loginDto)
        {
            var exists = _securityService.EmailExists(loginDto.Email);
            if(exists)
                 return BadRequest("Email already exists");
            return _securityService.Create(loginDto.Email, loginDto.Password);
        }

    }

    public class TokenDto
    {
        public string Jwt { get; set; }
        public string Message { get; set; }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
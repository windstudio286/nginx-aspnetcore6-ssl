using System;
using Microsoft.AspNetCore.Mvc;
using NginxDocker.Api.Services;
using System.Security.Claims;
using NginxDocker.Api.Models;
using NginxDocker.Api.Requests;
using NginxDocker.Api.Responses;
namespace NginxDocker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly WebApiDbContext _userContext;
        private readonly ITokenService _tokenService;

        public AuthController(WebApiDbContext userContext, ITokenService tokenService)
        {
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest is null)
            {
                return BadRequest("Invalid client request");
            }

            var user = _userContext.Users.FirstOrDefault(u =>
                (u.Username == loginRequest.UserName) && (u.Password == loginRequest.Password));
            if (user is null)
                return Unauthorized();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginRequest.UserName),
                new Claim(ClaimTypes.Role, "User")
            };
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            _userContext.SaveChanges();

            return Ok(new AuthenticatedResponse
            {
                Token = accessToken,
                RefreshToken = refreshToken
            });
        }
    }
}


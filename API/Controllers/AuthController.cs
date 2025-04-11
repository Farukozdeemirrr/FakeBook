using Business.Abstract;
using Business.Security.Abstarct;
using Business.Security.Concrate;
using DTO.Auth;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private IAuthService _authService;
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService, IAuthService authService)
        {
            _tokenService = tokenService;
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto userLoginDto)
        {
         var result = _authService.Login(userLoginDto);
            return Ok(result);
        }
        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserRegisterDto userRegisterDto)
        {
            var result = _authService.Register(userRegisterDto);
            return Ok(result);
        }
    }
}

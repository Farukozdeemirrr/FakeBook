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

        public AuthController(IAuthService authService)
        {  
            _authService = authService;
        }

        [HttpPost("Login")]
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

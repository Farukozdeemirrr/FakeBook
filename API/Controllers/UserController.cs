using Business.Abstract;
using DTO.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id:long}")]
        public IActionResult GetById(long id)
        {
            var userId = _userService.GetByUserId(id);
            return Ok(userId);
        }
        [Authorize]
        [HttpPut("{id:long}")]
        public IActionResult Update(long id, UserUpdateDto userUpdateDto)
        {
            var userUpdate = _userService.UserUpdate(id, userUpdateDto);
            return Ok(userUpdate);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("All")]
        public IActionResult GetAllUser()
        {
            var getAllUser = _userService.GetAllUser();
            return Ok(getAllUser);
        }
        
    }
}

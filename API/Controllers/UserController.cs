using Business.Abstract;
using DTO.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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


        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var userId = _userService.GetByUserId(id);
            return Ok(userId);
        }


        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(long id, UserUpdateDto userUpdateDto)
        {
            var userUpdate = _userService.UserUpdate(id, userUpdateDto);
            return Ok(userUpdate);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet()]
        public IActionResult GetAllUser()
        {
            var getAllUser = _userService.GetAllUser();
            return Ok(getAllUser);
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(long id)
        {  
            var deleteUser = _userService.UserDelete(id);
            return Ok(deleteUser);
        }
        
    }
}

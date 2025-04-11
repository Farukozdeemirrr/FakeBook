using Business.Abstract;
using DTO.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private IPostService _postservice;

        public PostController(IPostService postservice)
        {
            _postservice = postservice;
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreatePost([FromBody] PostCreateDto createDto)
        {
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = _postservice.CreatePost(userId, createDto);
            return Ok(result);
        }

    }
}

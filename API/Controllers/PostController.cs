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

        [Authorize]
        [HttpPost]
        public IActionResult CreatePost([FromBody] PostCreateDto createDto)
        {
            var result = _postservice.CreatePost(createDto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdatePost([FromBody] PostUpdateDto updateDto, long id)
        {
            var updatePost = _postservice.UpdatePost(updateDto);
            return Ok(updatePost);
        }

        [HttpDelete ("{id}")]
        [Authorize]
        public IActionResult DeletePost([FromRoute] long id)
        {
            _postservice.DeletePost(id);
            return Ok();
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetPost([FromRoute] long id) {
            var getPostById = _postservice.GetByPostId(id);
            return Ok(getPostById);
        }

        [HttpGet()]
        [Authorize]
        public IActionResult GetAllPost()
        {
            var getAllPost = _postservice.GetAllPosts();
            return Ok(getAllPost);
        }

    }
}

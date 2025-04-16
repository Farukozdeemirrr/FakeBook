using Business.Abstract;
using Business.Security.Abstarct;
using Business.Security.Concrate;
using DataAccess.Abstract;
using DTO.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
            
        }

        //[Authorize]
        //[HttpPost("{userId}")]
        //public IActionResult CreateCooment(
        //[FromRoute] long userId,
        //[FromQuery] long postId,
        //[FromBody] CommentCreateDto commentCreateDto)
        //{
        //    var commentCreate = _commentService.CreateCommet(userId, postId, commentCreateDto);
        //    return Ok(commentCreate);
        //}

        [Authorize]
        [HttpPost()]
        public IActionResult CreateComment([FromBody] CommentCreateDto commentCreateDto)
        { 
            var commentCreate = _commentService.CreateComment(commentCreateDto);
            return Ok(commentCreate);
        }


        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateComment([FromBody] CommentUpdateDto commentUpdateDto, long id)
        {
            var result = _commentService.UpdateComment(commentUpdateDto);
            return Ok(result);
        }

        [Authorize]
        [HttpGet()]
        public IActionResult GetAllComment()
        {
            var getAllComment = _commentService.GetAllComment();
            return Ok(getAllComment);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteComment(long id)
        {
            _commentService.DeleteComment(id);
            return Ok("Yorum silindi.");
        }

    }
}

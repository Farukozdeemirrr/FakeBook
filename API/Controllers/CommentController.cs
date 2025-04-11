using Business.Abstract;
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
        [HttpPost]
        public IActionResult CreateCooment(
        [FromQuery] long postId,
        [FromBody] CommentCreateDto commentCreateDto)
        {
            // ✅ Token içinden userId'yi çekiyoruz
            //BURASI NASIL OLUYOR DETAYLI BİR ŞEKİLDE SOR VEYA FARKLI BİR YOLU VAR MI??? 
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var commentCreate = _commentService.CreateCommet(userId, postId, commentCreateDto);
            return Ok(commentCreate);
        }


        [Authorize]
        [HttpPut("Put")]
        public IActionResult UpdateComment([FromBody] CommentCreateDto commentUpdateDto)
        {
            var updateComment = _commentService.UpdateComment(commentUpdateDto);
            return Ok(updateComment);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllComment()
        {
            var getAllComment = _commentService.GetAllComment();
            return Ok(getAllComment);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteComment([FromRoute] long id)
        {
            _commentService.DeleteComment(id);
            return NoContent(); // 204 No Content
        }


    }
}

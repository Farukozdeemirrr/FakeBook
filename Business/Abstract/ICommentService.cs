using DTO.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICommentService
    {
        List<CommentDto> GetAllComment();
        CommentCreateDto CreateCommet(long userId, long postId, CommentCreateDto commentCreateDto);
        CommentCreateDto UpdateComment(CommentCreateDto commentCreateDto);
        void DeleteComment(long id);
    }
}

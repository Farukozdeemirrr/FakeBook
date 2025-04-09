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
        List<CommentDto> GetAllComment(long id);
        CommentCreateDto CreateCommet(long userId, long postId, CommentCreateDto commentCreateDto);
        void DeleteComment(long id);
    }
}

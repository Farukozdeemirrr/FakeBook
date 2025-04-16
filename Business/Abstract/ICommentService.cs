using DTO.Comment;


namespace Business.Abstract
{
    public interface ICommentService
    {
        List<CommentDto> GetAllComment();
        CommentDto CreateComment(CommentCreateDto commentCreateDto);
        CommentDto UpdateComment(CommentUpdateDto dto);
        void DeleteComment(long commentId);

    }
}

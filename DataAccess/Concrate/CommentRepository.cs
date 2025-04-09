using DataAccess.Abstract;
using Entities;

namespace DataAccess.Concrate
{
    public class CommentRepository: BaseRepository<Comment>, ICommentRepository
    {
    }
}

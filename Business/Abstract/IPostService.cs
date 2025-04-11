using DTO.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPostService
    {
        List<PostDto> GetAllPosts();
        List<PostDto> GetByUserId(long userId);
        PostDto GetByPostId(long id);
        PostDto CreatePost(long userId, PostCreateDto createDto);
        PostDto UpdatePost(long id, PostCreateDto updateDto);
        void DeletePost(long id);
    }
}

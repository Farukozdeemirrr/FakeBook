using AutoMapper;
using Business.Abstract;
using DataAccess.Abstract;
using DTO.Post;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrate
{
    public class PostManager : IPostService
    {
        private IMapper _mapper;
        private IPostRepository _postRepository;

        public PostManager(IMapper mapper, IPostRepository repository) 
        {
            _mapper = mapper;
            _postRepository = repository;
        }

        public PostDto CreatePost(long userId, PostCreateDto createDto)
        {
            var entityPost = _mapper.Map<Post>(createDto);
            using (var context = new FakeBookDbContext())
            {
                var createPost = _postRepository.Add(context, entityPost);
                context.SaveChanges();
                return _mapper.Map<PostDto>(createPost);

            }
        }

        public void DeletePost(long id)
        {
            using (var context = new FakeBookDbContext())
            {
                _postRepository.Delete(context, id);
                context.SaveChanges();
            }
        }

        public List<PostDto> GetAllPosts()
        {
            using (var context = new FakeBookDbContext())
            {
                var postList = _postRepository.GetAll(context);
                return _mapper.Map<List<PostDto>>(postList);
            }
        }

        public PostDto GetByPostId(long id)
        {
            using (var context = new FakeBookDbContext())
            {
                var postId = _postRepository.GetById(context, id);
                return _mapper.Map<PostDto>(postId);
            }
        }

        public List<PostDto> GetByUserId(long userId)
        {
            using (var context = new FakeBookDbContext())
            {
                // İlgili userId'ye sahip postları çekiyoruz
                var userPosts = _postRepository
                    .GetAll(context)
                    .Where(p => p.UserId == userId)
                    .ToList();

                // DTO'ya mapliyoruz
                return _mapper.Map<List<PostDto>>(userPosts);
            }
        }


        public PostDto UpdatePost(long id, PostCreateDto updateDto)
        {
            var entityPost = _mapper.Map<Post>(updateDto);
            using (var context = new FakeBookDbContext())
            {
                var updatePost = _postRepository.Add(context, entityPost);
                context.SaveChanges();
                return _mapper.Map<PostDto>(updatePost);
            }
        }
    }
}

using AutoMapper;
using Business.Abstract;
using Business.Security.Abstarct;
using DataAccess.Abstract;
using DTO.Post;
using Entities;
using Microsoft.EntityFrameworkCore;
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
        private IUserClaim _userClaim;

        public PostManager(IMapper mapper, IPostRepository repository, IUserClaim userClaim)
        {
            _mapper = mapper;
            _postRepository = repository;
            _userClaim = userClaim;
        }

        //public PostDto CreatePost(long userId, PostCreateDto createDto)
        //{
        //    var entityPost = _mapper.Map<Post>(createDto);
        //    using (var context = new FakeBookDbContext())
        //    {
        //        var createPost = _postRepository.Add(context, entityPost);
        //        context.SaveChanges();
        //        return _mapper.Map<PostDto>(createPost);

        //    }
        //}
        public PostDto CreatePost(PostCreateDto createDto)
        {
            using (var context = new FakeBookDbContext())
            {
                // 1. DTO'dan Entity'ye dönüşüm
                var entityPost = _mapper.Map<Post>(createDto);

                // 2. Token'dan gelen UserId'yi ata
                entityPost.UserId = _userClaim.UserId;
                entityPost.CreatedAt = DateTime.UtcNow;

                // 3. Post'u veritabanına kaydet
                var createdPost = _postRepository.Add(context, entityPost);
                context.SaveChanges();

                // 4. User bilgilerini manuel olarak çek → çünkü AutoMapper User.FullName için bu veriye ihtiyaç duyar
                var postWithUser = context.Posts
                    .Include(p => p.User)
                    .FirstOrDefault(p => p.Id == createdPost.Id);

                // 5. DTO'ya map et ve dön
                return _mapper.Map<PostDto>(postWithUser);
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


        public PostDto UpdatePost(PostUpdateDto updateDto)
        {
            using (var context = new FakeBookDbContext())
            {
                // 1. İlgili postu kullanıcı bilgisiyle birlikte çekiyoruz
                var post = context.Posts
                    .Include(p => p.User)
                    .FirstOrDefault(p => p.Id == updateDto.Id);

                if (post == null)
                    throw new Exception("Post bulunamadı.");

                // 2. Sadece sahip olan kullanıcı ya da admin güncelleyebilir
                if (post.UserId != _userClaim.UserId && _userClaim.Role != "Admin")
                    throw new UnauthorizedAccessException("Bu gönderiyi güncelleme yetkiniz yok.");

                // 3. Güncelleme işlemi (null olmayan alanlar DTO'dan alınır)
                _mapper.Map(updateDto, post);

                // 4. Veritabanına işle
                context.SaveChanges();

                // 5. Cevap DTO’su olarak PostDto dön (User bilgisi de maplenecek şekilde)
                return _mapper.Map<PostDto>(post);
            }
        }



    }
}

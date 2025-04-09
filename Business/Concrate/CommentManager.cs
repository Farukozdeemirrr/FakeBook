using AutoMapper;
using Business.Abstract;
using Business.Validators.Auth;
using Business.Validators.Comment;
using DataAccess.Abstract;
using DTO.Comment;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrate
{
    public class CommentManager : ICommentService
    {


        private ICommentRepository _commentRepository;
        private IMapper _mapper;
        private readonly CommentCreateDtoValidator _validator;

        public CommentManager(
            ICommentRepository commentRepository,
            IMapper mapper,
            CommentCreateDtoValidator validator)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public CommentCreateDto CreateCommet(long userId, long postId, CommentCreateDto commentCreateDto)
        {
            using (var context = new FakeBookDbContext())
            {
                var entityComment = _mapper.Map<Comment>(commentCreateDto);
                var createComment = _commentRepository.Add(context, entityComment);
                context.SaveChanges();

                return _mapper.Map<CommentCreateDto>(createComment);
            }
        }

        public void DeleteComment(long id)
        {
            using (var context = new FakeBookDbContext())
            {
                _commentRepository.Delete(context, id);
                context.SaveChanges();
            }
        }

        public List<CommentDto> GetAllComment(long id)
        {
            using (var context = new FakeBookDbContext())
            {
                var commentQuery = _commentRepository.GetAll(context);
                var list = _mapper.ProjectTo<CommentDto>(commentQuery);
                return list.ToList();
            }
        }


    }
}

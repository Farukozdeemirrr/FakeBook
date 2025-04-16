using AutoMapper;
using Business.Abstract;
using Business.Security.Abstarct;
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
        private IUserClaim _userClaim;

        public CommentManager(
            ICommentRepository commentRepository,
            IMapper mapper,
            CommentCreateDtoValidator validator,
            IUserClaim userClaim
            )
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _validator = validator;
            _userClaim = userClaim;
        }


        public CommentDto CreateComment(CommentCreateDto commentCreateDto)
        {
            using (var context = new FakeBookDbContext())
            {
                var entity = _mapper.Map<Comment>(commentCreateDto);
                var created = _commentRepository.Add(context, entity);
                context.SaveChanges();

                return _mapper.Map<CommentDto>(created); // ✅ Uyumlu
            }
        }


        public void DeleteComment(long commentId)
        {
            using (var context = new FakeBookDbContext())
            {
                var comment = _commentRepository.GetById(context, commentId);
                if (comment == null)
                    throw new Exception("Yorum bulunamadı.");

                if (comment.UserId != _userClaim.UserId && _userClaim.Role != "Admin")
                    throw new UnauthorizedAccessException("Bu yorumu silme yetkiniz yok.");

                _commentRepository.Delete(context, commentId);
                context.SaveChanges();
            }
        }


        public List<CommentDto> GetAllComment()
        {
            using (var context = new FakeBookDbContext())
            {
                var commentQuery = _commentRepository.GetAll(context);
                var list = _mapper.ProjectTo<CommentDto>(commentQuery);
                return list.ToList();
            }
        }


        public CommentDto UpdateComment(CommentUpdateDto dto)
        {
            using (var context = new FakeBookDbContext())
            {
                var existing = _commentRepository.GetById(context, dto.Id);
                if (existing == null)
                    throw new Exception("Yorum bulunamadı.");


                if (existing.UserId != _userClaim.UserId && _userClaim.Role != "Admin")
                    throw new UnauthorizedAccessException("Bu yorumu güncelleme yetkiniz yok.");

                existing.Text = dto.Text ?? existing.Text;
                context.SaveChanges();

                return _mapper.Map<CommentDto>(existing);
            }
        }
    }
}


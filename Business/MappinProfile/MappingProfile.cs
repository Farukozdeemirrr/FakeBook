using AutoMapper;
using Business.Security.Abstarct;
using DTO.Auth;
using DTO.Comment;
using DTO.Post;
using DTO.User;
using Entities;

namespace Business.MappinProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // --- AUTH ---
            CreateMap<UserRegisterDto, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore()); // Şifre hash'lenmiş gelecek

            CreateMap<User, AuthResponseDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.userRole))
                .ForMember(dest => dest.Token, opt => opt.Ignore()); // Token sonradan eklenecek

            // --- USER ---
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<UserUpdateDto, User>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // --- POST ---
            CreateMap<PostCreateDto, Post>();

            CreateMap<Post, PostDto>()
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
                .ForMember(dest => dest.UserProfilePicture, opt => opt.MapFrom(src => src.User.ProfilePicture));

            CreateMap<PostUpdateDto, Post>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Post, PostUpdateDto>();

            // --- COMMENT ---
            CreateMap<CommentCreateDto, Comment>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom<UserIdResolver>())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<CommentUpdateDto, Comment>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
                .ForMember(dest => dest.UserProfilePicture, opt => opt.MapFrom(src => src.User.ProfilePicture));

            CreateMap<Comment, CommentUpdateDto>();
            CreateMap<Comment, CommentCreateDto>();
        }
    }
}

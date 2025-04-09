using AutoMapper;
using DTO.Auth;
using DTO.Comment;
using DTO.Post;
using DTO.User;
using Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Business.MappinProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Auth
            CreateMap<UserRegisterDto, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore()); // Şifre hash dışardan gelmeyecek

            // User
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<UserUpdateDto, User>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); // sadece null olmayanları eşleştir

            // Post
            CreateMap<PostCreateDto, Post>();

            CreateMap<Post, PostDto>()
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
                .ForMember(dest => dest.UserProfilePicture, opt => opt.MapFrom(src => src.User.ProfilePicture));

            // Comment
            CreateMap<CommentCreateDto, Comment>();

            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
                .ForMember(dest => dest.UserProfilePicture, opt => opt.MapFrom(src => src.User.ProfilePicture));
        }
    }
}

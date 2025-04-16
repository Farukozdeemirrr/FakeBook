using AutoMapper;
using DTO.Comment;
using Entities;
using Business.Security.Abstarct;

public class UserIdResolver : IValueResolver<CommentCreateDto, Comment, long>
{
    private readonly IUserClaim _userClaim;

    public UserIdResolver(IUserClaim userClaim)
    {
        _userClaim = userClaim;
    }

    public long Resolve(CommentCreateDto source, Comment destination, long destMember, ResolutionContext context)
    {
        return _userClaim.UserId;
    }
}

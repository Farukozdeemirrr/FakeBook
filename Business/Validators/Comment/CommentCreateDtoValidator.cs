using DTO.Comment;
using FluentValidation;

namespace Business.Validators.Comment
{
    public class CommentCreateDtoValidator : AbstractValidator<CommentCreateDto>
{
    public CommentCreateDtoValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Yorum boş olamaz.")
            .MaximumLength(500);
    }
}
}

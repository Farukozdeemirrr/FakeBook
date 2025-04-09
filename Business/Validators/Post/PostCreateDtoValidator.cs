
using DTO.Post;
using FluentValidation;

namespace Business.Validators.Post
{
    public class PostCreateDtoValidator : AbstractValidator<PostCreateDto>
{
    public PostCreateDtoValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Gönderi içeriği boş olamaz.")
            .MaximumLength(1000);

        RuleFor(x => x.ImageUrl)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.ImageUrl));
    }
}
}

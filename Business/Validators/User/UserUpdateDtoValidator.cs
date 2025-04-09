using DTO.User;
using FluentValidation;

namespace Business.Validators.User
{
    public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .MaximumLength(50);

            RuleFor(x => x.Bio)
                .MaximumLength(300);

            RuleFor(x => x.ProfilePicture)
                .MaximumLength(500)
                .When(x => !string.IsNullOrWhiteSpace(x.ProfilePicture));
        }
    }
}

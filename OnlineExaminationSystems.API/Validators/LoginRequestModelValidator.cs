using FluentValidation;
using OnlineExaminationSystems.API.Models.Dtos;

namespace OnlineExaminationSystems.API.Validators
{
    public class LoginRequestModelValidator : AbstractValidator<LoginRequestModel>
    {
        public LoginRequestModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Password is required.")
                                 .EmailAddress().WithMessage("Invalid email address.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
        }
    }
}

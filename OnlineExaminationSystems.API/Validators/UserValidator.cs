using FluentValidation;
using OnlineExaminationSystems.API.Models.Entities;
using OnlineExaminationSystems.API.Services.Abstract;

namespace OnlineExaminationSystems.API.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        private readonly IUsersService _userService;

        public UserValidator(IUsersService userService)
        {
            _userService = userService;

            RuleFor(model => model.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(model => model.Surname).NotEmpty().WithMessage("Surname is required.");

            RuleFor(model => model.Email).NotEmpty().WithMessage("Email is required.")
                                              .EmailAddress().WithMessage("Invalid email address.")
                                              .MustAsync(async (model, email, cancellation) => await _userService.IsUniqueEmailAsync(model.Id, email))
                                                .WithMessage("This email address is already registered.");

            RuleFor(model => model.Password).NotEmpty().WithMessage("Password is required.")
                                             .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                                             .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                                             .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                                             .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
                                             .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

            RuleFor(model => model.RoleId).NotEmpty().WithMessage("Role is required.")
                                        .Must(role => role == 1 || role == 2).WithMessage("Role can only be '1 (Academician)' or '2 (Student)'.");
        }
    }
}
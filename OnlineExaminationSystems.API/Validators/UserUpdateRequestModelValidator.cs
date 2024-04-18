using FluentValidation;
using OnlineExaminationSystems.API.Model.Dtos.User;
using OnlineExaminationSystems.API.Model.Entities;
using OnlineExaminationSystems.API.Model.Repository;

namespace OnlineExaminationSystems.API.Validators
{
    public class UserUpdateRequestModelValidator : AbstractValidator<UserUpdateRequestModel>
    {
        private readonly IGenericRepository<User> _repository;

        public UserUpdateRequestModelValidator(IGenericRepository<User> repository)
        {
            _repository = repository;

            RuleFor(model => model.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(model => model.Surname).NotEmpty().WithMessage("Surname is required.");

            RuleFor(model => model.Email).NotEmpty().WithMessage("Email is required.")
                                              .EmailAddress().WithMessage("Invalid email address.")
                                              .MustAsync(async (email, cancellation) => await IsUniqueEmailAsync(email))
                                                .WithMessage("This email address is already registered.");


            RuleFor(model => model.Password).NotEmpty().WithMessage("Password is required.")
                                             .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                                             .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                                             .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                                             .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
                                             .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

            RuleFor(model => model.Role).NotEmpty().WithMessage("Role is required.")
                                        .Must(role => role == "Academician" || role == "Student").WithMessage("Role can only be 'Academician' or 'Student'.");
        }
        private async Task<bool> IsUniqueEmailAsync(string email)
        {
            var query = $"SELECT * FROM USERS WHERE email = @Email";
            var parameters = new { Email = email };

            var existingUser = _repository.ExecuteQuery(query, parameters);
            return existingUser.Count() == 0;
        }
    }
}
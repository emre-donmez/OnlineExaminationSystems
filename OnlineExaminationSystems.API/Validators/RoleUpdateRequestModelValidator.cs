using FluentValidation;
using OnlineExaminationSystems.API.Models.Dtos;

namespace OnlineExaminationSystems.API.Validators
{
    public class RoleUpdateRequestModelValidator : AbstractValidator<RoleUpdateRequestModel>
    {
        public RoleUpdateRequestModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Role name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        }
    }
}

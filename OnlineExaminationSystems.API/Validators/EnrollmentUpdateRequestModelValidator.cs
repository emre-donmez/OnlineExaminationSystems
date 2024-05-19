using FluentValidation;
using OnlineExaminationSystems.API.Models.Dtos;

namespace OnlineExaminationSystems.API.Validators
{
    public class EnrollmentUpdateRequestModelValidator : AbstractValidator<EnrollmentUpdateRequestModel>
    {
        public EnrollmentUpdateRequestModelValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User Id is required");
            RuleFor(x => x.LessonId).NotEmpty().WithMessage("Lesson Id is required");
        }
    }
}

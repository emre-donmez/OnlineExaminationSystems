using FluentValidation;
using OnlineExaminationSystems.API.Models.Dtos;

namespace OnlineExaminationSystems.API.Validators
{
    public class ExamUpdateRequestModelValidator : AbstractValidator<ExamUpdateRequestModel>
    {
        public ExamUpdateRequestModelValidator()
        {
            RuleFor(x => x.LessonId).NotEmpty().WithMessage("Lesson id is required");
            RuleFor(x => x.QuestionCount).NotEmpty().WithMessage("Question count is required");
            RuleFor(x => x.Duration).NotEmpty().WithMessage("Duration is required");
            RuleFor(x => x.StartedDate).NotEmpty().WithMessage("Started date is required");
        }
    }
}
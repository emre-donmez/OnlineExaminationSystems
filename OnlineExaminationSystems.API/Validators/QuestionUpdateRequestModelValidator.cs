using FluentValidation;
using OnlineExaminationSystems.API.Models.Dtos;

namespace OnlineExaminationSystems.API.Validators
{
    public class QuestionUpdateRequestModelValidator : AbstractValidator<QuestionUpdateRequestModel>
    {
        public QuestionUpdateRequestModelValidator()
        {
            RuleFor(x => x.QuestionText).NotEmpty().WithMessage("Question text is required");
            RuleFor(x => x.Option1).NotEmpty().WithMessage("Option 1 is required");
            RuleFor(x => x.Option2).NotEmpty().WithMessage("Option 2 is required");
            RuleFor(x => x.Option3).NotEmpty().WithMessage("Option 3 is required");
            RuleFor(x => x.CorrectAnswer).NotEmpty().WithMessage("Correct answer is required");
            RuleFor(x => x.ExamId).NotEmpty().WithMessage("Exam id is required");
        }
    }
}

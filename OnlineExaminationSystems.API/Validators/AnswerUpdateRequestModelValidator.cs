using FluentValidation;
using OnlineExaminationSystems.API.Models.Dtos;

namespace OnlineExaminationSystems.API.Validators
{
    public class AnswerUpdateRequestModelValidator : AbstractValidator<AnswerUpdateRequestModel>
    {
        public AnswerUpdateRequestModelValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User id is required");
            RuleFor(x => x.QuestionId).NotEmpty().WithMessage("Question id is required");
            RuleFor(x => x.GivenAnswer).NotEmpty().WithMessage("Given answer is required");
        }
    }
}

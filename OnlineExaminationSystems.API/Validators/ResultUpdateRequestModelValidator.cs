using FluentValidation;
using OnlineExaminationSystems.API.Models.Dtos;

namespace OnlineExaminationSystems.API.Validators
{
    public class ResultUpdateRequestModelValidator : AbstractValidator<ResultUpdateRequestModel>
    {
        public ResultUpdateRequestModelValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User id is required");
            RuleFor(x => x.ExamId).NotEmpty().WithMessage("Exam id is required");
            RuleFor(x => x.Score).NotEmpty().WithMessage("Score is required");
        }
    }
}

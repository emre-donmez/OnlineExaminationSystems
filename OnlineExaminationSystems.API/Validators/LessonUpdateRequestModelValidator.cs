using FluentValidation;
using OnlineExaminationSystems.API.Models.Dtos;
using OnlineExaminationSystems.API.Services.Abstract;

namespace OnlineExaminationSystems.API.Validators
{
    public class LessonUpdateRequestModelValidator : AbstractValidator<LessonUpdateRequestModel>
    {
        private readonly ILessonsService _lessonsService;

        public LessonUpdateRequestModelValidator(ILessonsService lessonsService)
        {
            _lessonsService = lessonsService;

            RuleFor(x => x.Name).NotEmpty().WithMessage("Lesson name is required");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User is requried")
                                    .MustAsync(async (id, cancellation) => await _lessonsService.IsAnyUser(id))
                                                .WithMessage("There are no users with this id.");
        }
    }
}
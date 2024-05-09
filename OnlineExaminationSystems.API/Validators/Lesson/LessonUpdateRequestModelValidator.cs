using FluentValidation;
using OnlineExaminationSystems.API.Models.Dtos.User;
using OnlineExaminationSystems.API.Services.Abstract;

namespace OnlineExaminationSystems.API.Validators.Lesson
{
    public class LessonUpdateRequestModelValidator : AbstractValidator<LessonUpdateRequestModel>
    {
        private readonly ILessonsService _lessonService;
        public LessonUpdateRequestModelValidator(ILessonsService lessonsService)
        {
            _lessonService = lessonsService;

            RuleFor(model => model.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(model => model.UserId).NotEmpty().WithMessage("User ID is required.")
                                           .MustAsync(async (id,cancellation) => await _lessonService.IsAnyUser(id))
                                           .WithMessage("No user with this id was found.");
        }
    }
}

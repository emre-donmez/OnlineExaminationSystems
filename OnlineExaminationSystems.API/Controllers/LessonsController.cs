using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.API.Models.Dtos;
using OnlineExaminationSystems.API.Services.Abstract;

namespace OnlineExaminationSystems.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonsService _lessonsService;
        private readonly IValidator<LessonUpdateRequestModel> _validatorLessonUpdateRequest;
        private readonly IExamsService _examsService;

        public LessonsController(ILessonsService lessonsService, IValidator<LessonUpdateRequestModel> validatorLessonUpdateRequest, IExamsService examsService)
        {
            _lessonsService = lessonsService;
            _validatorLessonUpdateRequest = validatorLessonUpdateRequest;
            _examsService = examsService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var lessons = _lessonsService.GetAll();
            return Ok(lessons);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var lesson = _lessonsService.GetById(id);
            return lesson != null ? Ok(lesson) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LessonUpdateRequestModel model)
        {
            var validationResult = await _validatorLessonUpdateRequest.ValidateAsync(model);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var lesson = _lessonsService.Create(model);
            return CreatedAtAction(nameof(Get), new { id = lesson.Id }, lesson);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, LessonUpdateRequestModel model)
        {
            var validationResult = await _validatorLessonUpdateRequest.ValidateAsync(model);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var lesson = _lessonsService.Update(id, model);
            return Ok(lesson);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _lessonsService.Delete(id);
            return result ? Ok() : NotFound();
        }

        [HttpGet("{id}/exams")]
        public IActionResult GetExamsByLessonId(int id)
        {
            var exams = _examsService.GetExamsByLessonId(id);
            return Ok(exams);
        }
    }
}
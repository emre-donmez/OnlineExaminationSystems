using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.API.Models.Dtos.User;
using OnlineExaminationSystems.API.Services.Abstract;

namespace OnlineExaminationSystems.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonsService _lessonsService;

        public LessonsController(ILessonsService lessonsService)
        {
            _lessonsService = lessonsService;
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
        public IActionResult Create(LessonUpdateRequestModel model)
        {
            var lesson = _lessonsService.Create(model);
            return CreatedAtAction(nameof(Get), new { id = lesson.Id }, lesson);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, LessonUpdateRequestModel model)
        {
            var lesson = _lessonsService.Update(id, model);
            return Ok(lesson);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _lessonsService.Delete(id);

            return result ? Ok() : NotFound(); 
        }
    }
}
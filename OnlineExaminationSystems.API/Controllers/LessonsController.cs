using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.API.Models.Dtos;
using OnlineExaminationSystems.API.Models.Helpers;
using OnlineExaminationSystems.API.Services.Abstract;

namespace OnlineExaminationSystems.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LessonsController : ControllerBase
{
    private readonly ILessonsService _lessonsService;
    private readonly IValidator<LessonUpdateRequestModel> _validatorLessonUpdateRequest;
    private readonly IExamsService _examsService;
    private readonly IEnrollmentsService _enrollmentsService;

    public LessonsController(ILessonsService lessonsService, IValidator<LessonUpdateRequestModel> validatorLessonUpdateRequest, IExamsService examsService, IEnrollmentsService enrollmentsService)
    {
        _lessonsService = lessonsService;
        _validatorLessonUpdateRequest = validatorLessonUpdateRequest;
        _examsService = examsService;
        _enrollmentsService = enrollmentsService;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = Roles.Admin)]
    public IActionResult Get(int id)
    {
        var lesson = _lessonsService.GetById(id);
        return lesson != null ? Ok(lesson) : NotFound();
    }

    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> Create(LessonUpdateRequestModel model)
    {
        var validationResult = await _validatorLessonUpdateRequest.ValidateAsync(model);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var lesson = _lessonsService.Create(model);
        return CreatedAtAction(nameof(Get), new { id = lesson.Id }, lesson);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> Update(int id, LessonUpdateRequestModel model)
    {
        var validationResult = await _validatorLessonUpdateRequest.ValidateAsync(model);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var lesson = _lessonsService.Update(id, model);
        return Ok(lesson);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = Roles.Admin)]
    public IActionResult Delete(int id)
    {
        var result = _lessonsService.Delete(id);
        return result ? Ok() : NotFound();
    }

    [HttpGet("{id}/exams")]
    [Authorize(Roles = Roles.Student)]// student or academician
    public IActionResult GetExamsByLessonId(int id)
    {
        var exams = _examsService.GetExamsByLessonId(id);
        return Ok(exams);
    }

    [HttpGet("{id}/students")]
    [Authorize(Roles = Roles.Admin)]
    public IActionResult GetStudentsWithUserByLessonId(int id)
    {
        var users = _enrollmentsService.GetStudentsWithUserByLessonId(id);
        return Ok(users);
    }

    [HttpGet("with-user")]
    [Authorize(Roles = Roles.Admin)]
    public IActionResult GetWithUser()
    {
        var lessons = _lessonsService.GetAllWithUser();
        return Ok(lessons);
    }
}
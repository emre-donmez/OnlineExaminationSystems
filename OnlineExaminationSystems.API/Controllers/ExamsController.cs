using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.API.Models.Dtos;
using OnlineExaminationSystems.API.Models.Helpers;
using OnlineExaminationSystems.API.Services.Abstract;

namespace OnlineExaminationSystems.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExamsController : ControllerBase
{
    private readonly IExamsService _examsService;
    private readonly IValidator<ExamUpdateRequestModel> _validatorExamUpdateRequestModel;
    private readonly IQuestionsService _questionsService;
    private readonly IResultsService _resultsService;

    public ExamsController(IExamsService examsService, IValidator<ExamUpdateRequestModel> validatorExamUpdateRequestModel, IQuestionsService questionsService, IResultsService resultsService)
    {
        _examsService = examsService;
        _validatorExamUpdateRequestModel = validatorExamUpdateRequestModel;
        _questionsService = questionsService;
        _resultsService = resultsService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var exams = _examsService.GetAll();
        return Ok(exams);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var exam = _examsService.GetById(id);
        return exam != null ? Ok(exam) : NotFound();
    }

    [HttpPost]
    [Authorize(Roles = Roles.Academician)]
    public async Task<IActionResult> Create(ExamUpdateRequestModel model)
    {
        var validationResult = await _validatorExamUpdateRequestModel.ValidateAsync(model);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var exam = _examsService.Create(model);
        return CreatedAtAction(nameof(Get), new { id = exam.Id }, exam);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = Roles.Academician)]
    public async Task<IActionResult> Update(int id, ExamUpdateRequestModel model)
    {
        var validationResult = await _validatorExamUpdateRequestModel.ValidateAsync(model);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var exam = _examsService.Update(id, model);
        return Ok(exam);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = Roles.Academician)]
    public IActionResult Delete(int id)
    {
        var result = _examsService.Delete(id);
        return result ? Ok() : NotFound();
    }

    [HttpGet("{examId}/questions")]
    [Authorize(Roles = Roles.Academician)]
    public IActionResult GetQuestionsByExamId(int examId)
    {
        var questions = _questionsService.GetQuestionsByExamId(examId);
        return Ok(questions);
    }

    [HttpGet("{examId}/start")]
    [Authorize(Roles = Roles.Student)]
    public IActionResult GetQuestionsByExamIdForExam(int examId)
    {
        var questions = _questionsService.GetQuestionsByExamIdForExam(examId);
        return Ok(questions);
    }

    [HttpGet("{examId}/results-with-user-and-exam")]
    [Authorize(Roles = Roles.Academician)]
    public IActionResult GetResultsWithUserAndExamByExamId(int examId)
    {
        var results = _resultsService.GetResultsWithUserAndExamByExamId(examId);
        return Ok(results);
    }

    [HttpGet("{examId}/calculate-results")]
    [Authorize(Roles = Roles.Academician)]
    public IActionResult CalculateResults(int examId)
    {
        _resultsService.CalculateResults(examId);
        return Ok();
    }
}
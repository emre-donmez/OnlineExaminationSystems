using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.UI.Areas.Academician.Models.Exam;
using OnlineExaminationSystems.UI.Areas.Mutual.Models.Lesson;
using OnlineExaminationSystems.UI.Helpers;

namespace OnlineExaminationSystems.UI.Areas.Academician.Controllers;

[Area("Academician")]
public class ExamController : Controller
{
    private readonly IApiRequestHelper _apiRequestHelper;

    public ExamController(IApiRequestHelper apiRequestHelper)
    {
        _apiRequestHelper = apiRequestHelper;
    }

    public async Task<IActionResult> Index(Lesson lesson)
    {
        var exams = await _apiRequestHelper.GetAsync<IEnumerable<Exam>>(ApiEndpoints.GetExamsByLessonIdEndPoint(lesson.Id));

        ViewBag.Lesson = lesson;

        return View(exams);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ExamUpdateRequestModel exam)
    {
        var exams = await _apiRequestHelper.PostAsync<Exam>(ApiEndpoints.ExamEndpoint, exam);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromBody] Exam model)
    {
        var response = await _apiRequestHelper.PutAsync<Exam>(ApiEndpoints.GetExamById(model.Id), model);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] ExamDeleteRequest model)
    {
        var response = await _apiRequestHelper.DeleteAsync(ApiEndpoints.GetExamById(model.Id));
        return response ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
    }
}
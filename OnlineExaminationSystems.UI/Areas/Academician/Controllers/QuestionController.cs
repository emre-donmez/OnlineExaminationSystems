using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.UI.Areas.Academician.Models.Question;
using OnlineExaminationSystems.UI.Helpers;

namespace OnlineExaminationSystems.UI.Areas.Academician.Controllers;

[Area("Academician")]
public class QuestionController : Controller
{
    private readonly IApiRequestHelper _apiRequestHelper;

    public QuestionController(IApiRequestHelper apiRequestHelper)
    {
        _apiRequestHelper = apiRequestHelper;
    }

    public async Task<IActionResult> Index(int examId)
    {
        var questions = await _apiRequestHelper.GetAsync<IEnumerable<Question>>(ApiEndpoints.GetQuestionsByExamIdEndPoint(examId));

        TempData["ExamId"] = examId;
        return View(questions);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] QuestionUpdateRequestModel question)
    {
        var createQuestion = await _apiRequestHelper.PostAsync<Question>(ApiEndpoints.QuestionEndpoint, question);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromBody] Question model)
    {
        var response = await _apiRequestHelper.PutAsync<Question>(ApiEndpoints.QuestionsEndPointWithId(model.Id), model);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] Question model)
    {
        var response = await _apiRequestHelper.DeleteAsync(ApiEndpoints.QuestionsEndPointWithId(model.Id));
        return response ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
    }
}
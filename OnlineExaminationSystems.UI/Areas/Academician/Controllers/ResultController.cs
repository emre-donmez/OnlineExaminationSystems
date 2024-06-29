using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.UI.Areas.Academician.Models.Result;
using OnlineExaminationSystems.UI.Helpers;

namespace OnlineExaminationSystems.UI.Areas.Academician.Controllers;

[Area("Academician")]
public class ResultController : Controller
{
    private readonly IApiRequestHelper _apiRequestHelper;

    public ResultController(IApiRequestHelper apiRequestHelper)
    {
        _apiRequestHelper = apiRequestHelper;
    }

    public async Task<IActionResult> Index(int examId)
    {
        var results = await _apiRequestHelper.GetAsync<IEnumerable<ResultWithUserAndExam>>(ApiEndpoints.GetResultsByExamIdEndPoint(examId));
        TempData["examId"] = examId;
        return View(results);
    }

    [HttpPost]
    public async Task<IActionResult> CalculateResult([FromBody] int examId)
    {
        await _apiRequestHelper.GetAsync<object>(ApiEndpoints.CalculateResultEndPoint(examId));
        return Ok();
    }
}
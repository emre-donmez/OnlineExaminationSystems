using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.UI.Areas.Academician.Models.Exam;
using OnlineExaminationSystems.UI.Areas.Academician.Models.Question;
using OnlineExaminationSystems.UI.Areas.Student.Models.Exam;
using OnlineExaminationSystems.UI.Helpers;

namespace OnlineExaminationSystems.UI.Areas.Student.Controllers;

[Area("Student")]
public class ExamController : Controller
{
    private readonly IApiRequestHelper _apiRequestHelper;

    public ExamController(IApiRequestHelper apiRequestHelper)
    {
        _apiRequestHelper = apiRequestHelper;
    }

    public async Task<IActionResult> Index(int lessonId)
    {
        var exams = await _apiRequestHelper.GetAsync<IEnumerable<Exam>>(ApiEndpoints.GetExamsByLessonIdEndPoint(lessonId));
        return View(exams);
    }

    public async Task<IActionResult> Exam(Exam exam)
    {
        ViewBag.ExamName = exam.Name;
        ViewBag.Duration = exam.Duration;
        var questions = await _apiRequestHelper.GetAsync<List<QuestionForExam>>(ApiEndpoints.GetQuestionsByExamIdForExam(exam.Id));
        return View(questions);
    }

    [HttpPost]
    public async Task<IActionResult> SubmitExam([FromBody] List<AnswerSubmitRequestModel> givenAnswers)
    {
        var userId = UserHelper.GetUserId(HttpContext);

        givenAnswers = givenAnswers.Select(x => new AnswerSubmitRequestModel
        {
            QuestionId = x.QuestionId,
            GivenAnswer = x.GivenAnswer,
            UserId = userId
        }).ToList();

        await _apiRequestHelper.PostAsync(ApiEndpoints.AnswerBulkEndpoint, givenAnswers);

        return Ok();
    }
}
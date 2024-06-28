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

    public async Task<IActionResult> Exam(int examId)
    {
        var exam = await _apiRequestHelper.GetAsync<Exam>(ApiEndpoints.GetExamById(examId));
        ViewBag.ExamName = exam.Name;
        ViewBag.Duration = exam.Duration;
        var questions = await _apiRequestHelper.GetAsync<List<QuestionForExam>>(ApiEndpoints.GetQuestionsByExamIdForExam(examId));
        return View(questions);
    }

    [HttpPost]
    public async Task<IActionResult> SubmitExam([FromBody] List<AnswerSubmitRequestModel> givenAnswers)
    {
        var userId = UserHelper.GetUserId(HttpContext);
        foreach (var givenAnswer in givenAnswers)
        {
            givenAnswer.UserId = userId;
            await _apiRequestHelper.PostAsync<AnswerSubmitRequestModel>(ApiEndpoints.AnswerEndPoint, givenAnswer);
        }

        return Ok();
    }
}

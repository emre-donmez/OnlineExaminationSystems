using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.UI.Helpers;
using OnlineExaminationSystems.UI.Models;
using OnlineExaminationSystems.UI.Models.Dtos;

namespace OnlineExaminationSystems.UI.Controllers
{
    public class StudentController : Controller
    {
        private readonly IApiRequestHelper _apiRequestHelper;

        public StudentController(IApiRequestHelper apiRequestHelper)
        {
            _apiRequestHelper = apiRequestHelper;
        }
        [Route("ExamPage")]
        public async Task<IActionResult> ExamPage(int examId)
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
            foreach (var givenAnswer in givenAnswers)
            {
                await _apiRequestHelper.PostAsync<AnswerSubmitRequestModel>(ApiEndpoints.AnswerEndPoint, givenAnswer);
            }            

            return RedirectToAction("Index","Home"); // burada öğrenci derslerine yönlendirilebilir.
        }   
    }
}

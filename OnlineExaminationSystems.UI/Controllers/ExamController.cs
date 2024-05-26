using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.UI.Helpers;
using OnlineExaminationSystems.UI.Models;
using OnlineExaminationSystems.UI.Models.Dtos;
using OnlineExaminationSystems.UI.Models.User;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OnlineExaminationSystems.UI.Controllers
{
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
        public async Task<IActionResult> Results(int examId)
        {
            var results = await _apiRequestHelper.GetAsync<IEnumerable<Result>>(ApiEndpoints.GetResultsByExamIdEndPoint(examId));
            var users = await _apiRequestHelper.GetAsync<IEnumerable<User>>(ApiEndpoints.UserEndpoint);
            var exams = await _apiRequestHelper.GetAsync<IEnumerable<Exam>>(ApiEndpoints.ExamEndpoint);
            foreach (var result in results)
            {
                result.User = users.FirstOrDefault(x => x.Id == result.UserId);
                result.Exam = exams.FirstOrDefault(x => x.Id == result.ExamId);
            }
            return View(results);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExamUpdateRequestModel exam)
        {
            var exams = await _apiRequestHelper.PostAsync<Exam>(ApiEndpoints.ExamEndpoint,exam);
            return RedirectToAction("Exams");
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

        public async Task<IActionResult> Exam(int ExamId)
        {
            var questions= await _apiRequestHelper.GetAsync<List<QuestionForExam>>(ApiEndpoints.QuestionEndpoint,ExamId);
            return View(questions);
        }






       


    }
}

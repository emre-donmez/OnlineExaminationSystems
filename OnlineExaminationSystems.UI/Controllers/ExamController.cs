using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.UI.Helpers;
using OnlineExaminationSystems.UI.Models;
using OnlineExaminationSystems.UI.Models.Dtos;
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
        public async Task<IActionResult> Lessons()
        {
            var lessons = await _apiRequestHelper.GetAsync<IEnumerable<Lesson>>(ApiEndpoints.LessonEndpoint);
            return View(lessons);
        }

        public async Task<IActionResult> Exams(int lessonId)
        {
            var exams = await _apiRequestHelper.GetAsync<IEnumerable<Exam>>(ApiEndpoints.GetExamsByLessonIdEndPoint(lessonId));
            return View(exams);
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
       
        public async Task<IActionResult> Edit(int id)
        {
            var exam = await _apiRequestHelper.GetAsync<Exam>($"{ApiEndpoints.ExamEndpoint}/{id}");
            return View(exam);
        }

        [HttpGet]
        public async Task<IActionResult> Questions(int examId)
        {
            var questions = await _apiRequestHelper.GetAsync<IEnumerable<Question>>(ApiEndpoints.GetQuestionsByExamIdEndPoint(examId));
            return View(questions);
        }
        public async Task<IActionResult> CreateQuestion(int examId)
        {
            ViewBag.ExamId = examId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateQuestion(QuestionUpdateRequestModel question)
        {
            var createQuestion = await _apiRequestHelper.PostAsync<Question>(ApiEndpoints.QuestionEndpoint, question);
            return RedirectToAction("Questions", new {examId=question.ExamId});
        }

        public async Task<IActionResult> Exam(int ExamId)
        {
            var questions= await _apiRequestHelper.GetAsync<List<QuestionForExam>>(ApiEndpoints.QuestionEndpoint,ExamId);
            return View(questions);
        }




       


    }
}

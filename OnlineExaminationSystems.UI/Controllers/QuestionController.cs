using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.UI.Helpers;
using OnlineExaminationSystems.UI.Models;
using OnlineExaminationSystems.UI.Models.Dtos;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OnlineExaminationSystems.UI.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IApiRequestHelper _apiRequestHelper;

        public QuestionController(IApiRequestHelper apiRequestHelper)
        {
            _apiRequestHelper = apiRequestHelper;
        }
        [Route("Questions")]
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
            return RedirectToAction("Questions", new { examId = question.ExamId });
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] Question model)
        {
            var response = await _apiRequestHelper.PutAsync<Question>(ApiEndpoints.GetQuestionsByExamIdEndPoint(model.Id), model);
            return Ok();
        }
      

    }
}

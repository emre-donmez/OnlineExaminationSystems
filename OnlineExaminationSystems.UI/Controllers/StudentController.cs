﻿using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        public async Task<IActionResult> ExamPage(int examId)
        {
           var exam = await _apiRequestHelper.GetAsync<Exam>(ApiEndpoints.GetExamById(examId));
            ViewBag.ExamName = exam.Name;
            ViewBag.Duration = exam.Duration;
            var questions = await _apiRequestHelper.GetAsync<List<QuestionForExam>>(ApiEndpoints.GetQuestionsByExamIdForExam(examId));
            return View(questions);
        }
        [HttpPost]
        public async Task<IActionResult> SubmitExam(AnswerSubmitRequestModel givenAnswer)
        {
            givenAnswer.UserId = 2;
            var answers = await _apiRequestHelper.PostAsync<AnswerSubmitRequestModel>(ApiEndpoints.AnswerEndPoint, givenAnswer);
            

            return RedirectToAction("ResultPage","Student");
        }   





}
}

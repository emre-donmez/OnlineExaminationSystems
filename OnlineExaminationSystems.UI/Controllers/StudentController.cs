using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.UI.Areas.Mutual.Models.Lesson;
using OnlineExaminationSystems.UI.Helpers;
using OnlineExaminationSystems.UI.Models.Dtos;
using OnlineExaminationSystems.UI.Models.Enrollment;
using OnlineExaminationSystems.UI.Models.Exam;
using OnlineExaminationSystems.UI.Models.Question;

namespace OnlineExaminationSystems.UI.Controllers
{
    public class StudentController : Controller
    {
        private readonly IApiRequestHelper _apiRequestHelper;

        public StudentController(IApiRequestHelper apiRequestHelper)
        {
            _apiRequestHelper = apiRequestHelper;
        }

        public async Task<IActionResult> Lessons(int userId)
        {
            var enrollments = await _apiRequestHelper.GetAsync<IEnumerable<Enrollment>>(ApiEndpoints.GetEnrollmentByUserIdEndPoint(userId));
            var lessons = await _apiRequestHelper.GetAsync<IEnumerable<Lesson>>(ApiEndpoints.LessonEndpoint);

            foreach (var enrollment in enrollments)
            {
                enrollment.Lesson = lessons.FirstOrDefault(x => x.Id == enrollment.LessonId);
            }
            return View(enrollments);
        }

        public async Task<IActionResult> Exams(int lessonId)
        {
            var exams = await _apiRequestHelper.GetAsync<IEnumerable<Exam>>(ApiEndpoints.GetExamsByLessonIdEndPoint(lessonId));
            return View(exams);
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

            return Ok();
        }
    }
}
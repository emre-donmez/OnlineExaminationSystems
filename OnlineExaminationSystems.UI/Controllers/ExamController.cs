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

        public async Task<IActionResult> Exams()
        {
            var exams = await _apiRequestHelper.GetAsync<IEnumerable<Exam>>(ApiEndpoints.ExamEndpoint);
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

    }
}

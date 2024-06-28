using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.UI.Areas.Mutual.Models.Lesson;
using OnlineExaminationSystems.UI.Helpers;


namespace OnlineExaminationSystems.UI.Areas.Academician.Controllers
{
    [Area("Academician")]
    public class LessonController : Controller
    {
        private readonly IApiRequestHelper _apiRequestHelper;
        public LessonController(IApiRequestHelper apiRequestHelper)
        {
            _apiRequestHelper = apiRequestHelper;
        }

        public async Task<IActionResult> Index()
        {
            var lessons = await _apiRequestHelper.
                GetAsync<IEnumerable<Lesson>>(
                ApiEndpoints.GetAcademicianLessonsByUserId(UserHelper.GetUserId(HttpContext)));

            return View(lessons);
        }
    }
}

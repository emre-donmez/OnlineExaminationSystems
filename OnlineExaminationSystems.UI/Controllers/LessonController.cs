using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.UI.Helpers;
using OnlineExaminationSystems.UI.Models.Lesson;
using OnlineExaminationSystems.UI.Models.User;

namespace OnlineExaminationSystems.UI.Controllers
{
    public class LessonController : Controller
    {
        private readonly IApiRequestHelper _apiRequestHelper;
        public LessonController(IApiRequestHelper apiRequestHelper)
        {
            _apiRequestHelper = apiRequestHelper;
        }

        public async Task<IActionResult> Index()
        {
            var lessons = await _apiRequestHelper.GetAsync<IEnumerable<Lesson>>(ApiEndpoints.LessonEndpoint);
            var users = await _apiRequestHelper.GetAsync<IEnumerable<User>>(ApiEndpoints.UserEndpoint);

            foreach (var lesson in lessons)
            {
                lesson.User = users.FirstOrDefault(x => x.Id == lesson.UserId);
            }

            ViewBag.Users = users;
            return View(lessons);
        }
    }
}

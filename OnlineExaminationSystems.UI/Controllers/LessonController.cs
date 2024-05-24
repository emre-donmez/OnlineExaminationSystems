using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.UI.Helpers;
using OnlineExaminationSystems.UI.Models;
using OnlineExaminationSystems.UI.Models.Dtos;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace OnlineExaminationSystems.UI.Controllers
{
   
    public class LessonController : Controller
    {
        private readonly IApiRequestHelper _apiRequestHelper;

        public LessonController(IApiRequestHelper apiRequestHelper)
        {
            _apiRequestHelper = apiRequestHelper;
        }
        [Route("Lessons")]
        public async Task<IActionResult> Lessons()
        {
            var lessons = await _apiRequestHelper.GetAsync<IEnumerable<Lesson>>(ApiEndpoints.LessonEndpoint);
            return View(lessons);
        }
    

    }
}

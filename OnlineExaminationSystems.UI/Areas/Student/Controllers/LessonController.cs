using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.UI.Areas.Mutual.Models.Enrollment;
using OnlineExaminationSystems.UI.Helpers;

namespace OnlineExaminationSystems.UI.Areas.Student.Controllers;

[Area("Student")]
public class LessonController : Controller
{
    private readonly IApiRequestHelper _apiRequestHelper;

    public LessonController(IApiRequestHelper apiRequestHelper)
    {
        _apiRequestHelper = apiRequestHelper;
    }

    public async Task<IActionResult> Index()
    {
        var enrollments = await _apiRequestHelper.GetAsync<IEnumerable<Enrollment>>(ApiEndpoints.GetEnrollmentByUserIdEndPoint(UserHelper.GetUserId(HttpContext)));

        return View(enrollments);
    }
}
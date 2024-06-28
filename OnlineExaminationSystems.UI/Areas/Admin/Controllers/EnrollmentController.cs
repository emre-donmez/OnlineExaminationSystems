using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.UI.Areas.Admin.Models.User;
using OnlineExaminationSystems.UI.Areas.Mutual.Models.Enrollment;
using OnlineExaminationSystems.UI.Helpers;

namespace OnlineExaminationSystems.UI.Areas.Admin.Controllers;

[Area("Admin")]
public class EnrollmentController : Controller
{
    private readonly IApiRequestHelper _apiRequestHelper;

    public EnrollmentController(IApiRequestHelper apiRequestHelper)
    {
        _apiRequestHelper = apiRequestHelper;
    }
    public async Task<IActionResult> GetAll()
    {
        var enrollments = await _apiRequestHelper.GetAsync<IEnumerable<Enrollment>>(ApiEndpoints.EnrollmentEndpoint);

        return Json(enrollments);
    }

    public async Task<IActionResult> GetStudentsByLessonId(int lessonId)
    {
        var enrollments = await _apiRequestHelper.GetAsync<IEnumerable<Enrollment>>(ApiEndpoints.GetStudentsByLessonIdEndpoint(lessonId));
        var users = await _apiRequestHelper.GetAsync<IEnumerable<User>>(ApiEndpoints.UserEndpoint);

        foreach (var enrollment in enrollments)
        {
            enrollment.User = users.FirstOrDefault(x => x.Id == enrollment.UserId);
        }

        return Json(enrollments);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] List<EnrollmentUpdateRequestModel> models)
    {
        foreach (var model in models)
        {
            await _apiRequestHelper.PostAsync<Enrollment>(ApiEndpoints.EnrollmentEndpoint, model);
        }
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] List<int> models)
    {
        foreach (var model in models)
        {
            await _apiRequestHelper.DeleteAsync(ApiEndpoints.EnrollmentEndPointWithId(model));
        }

        return Ok();
    }
}

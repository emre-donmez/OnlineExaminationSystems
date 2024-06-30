using Microsoft.AspNetCore.Mvc;
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

        return Json(enrollments);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] List<EnrollmentUpdateRequestModel> models)
    {
        await _apiRequestHelper.PostAsync(ApiEndpoints.EnrollmentBulkEndpoint, models);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] List<int> models)
    {
        await _apiRequestHelper.DeleteAsync(ApiEndpoints.EnrollmentBulkEndpoint, models);
        return Ok();
    }
}
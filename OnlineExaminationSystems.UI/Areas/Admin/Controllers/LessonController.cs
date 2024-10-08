﻿using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.UI.Areas.Admin.Models.User;
using OnlineExaminationSystems.UI.Areas.Mutual.Models.Lesson;
using OnlineExaminationSystems.UI.Helpers;

namespace OnlineExaminationSystems.UI.Areas.Admin.Controllers;

[Area("Admin")]
public class LessonController : Controller
{
    private readonly IApiRequestHelper _apiRequestHelper;

    public LessonController(IApiRequestHelper apiRequestHelper)
    {
        _apiRequestHelper = apiRequestHelper;
    }

    public async Task<IActionResult> Index()
    {
        var lessons = await _apiRequestHelper.GetAsync<IEnumerable<LessonWithUser>>(ApiEndpoints.GetLessonsWithUserEndPoint);
        var users = await _apiRequestHelper.GetAsync<IEnumerable<User>>(ApiEndpoints.UserEndpoint);

        ViewBag.Users = users;
        return View(lessons);
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromBody] Lesson model)
    {
        var response = await _apiRequestHelper.PutAsync<Lesson>(ApiEndpoints.LessonEndPointWithId(model.Id), model);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] LessonUpdateRequestModel model)
    {
        var response = await _apiRequestHelper.PostAsync<Lesson>(ApiEndpoints.LessonEndpoint, model);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] LessonDeleteRequest model)
    {
        var response = await _apiRequestHelper.DeleteAsync(ApiEndpoints.LessonEndPointWithId(model.Id));
        return response ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
    }
}
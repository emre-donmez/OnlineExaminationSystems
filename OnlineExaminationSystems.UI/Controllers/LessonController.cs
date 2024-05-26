﻿using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.UI.Helpers;
namespace OnlineExaminationSystems.UI.Controllers
{
   
    public class LessonController : Controller
    {
        private readonly IApiRequestHelper _apiRequestHelper;

        public LessonController(IApiRequestHelper apiRequestHelper)
        {
            _apiRequestHelper = apiRequestHelper;
        }
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
}

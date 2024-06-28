using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.UI.Areas.Admin.Models.User;
using OnlineExaminationSystems.UI.Helpers;
using OnlineExaminationSystems.UI.Models.Role;

namespace OnlineExaminationSystems.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IApiRequestHelper _apiRequestHelper;

        public UserController(IApiRequestHelper apiRequestHelper)
        {
            _apiRequestHelper = apiRequestHelper;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _apiRequestHelper.GetAsync<IEnumerable<User>>(ApiEndpoints.UserEndpoint);
            var roles = await _apiRequestHelper.GetAsync<IEnumerable<Role>>(ApiEndpoints.RoleEndpoint);

            foreach (var user in users)
            {
                user.Role = roles.FirstOrDefault(x => x.Id == user.RoleId);
            }

            ViewBag.Roles = roles;
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] User model)
        {
            var response = await _apiRequestHelper.PutAsync<User>(ApiEndpoints.UserEndPointWithId(model.Id), model);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserUpdateRequest model)
        {
            var response = await _apiRequestHelper.PostAsync<User>(ApiEndpoints.UserEndpoint, model);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] UserDeleteRequest model)
        {
            var response = await _apiRequestHelper.DeleteAsync(ApiEndpoints.UserEndPointWithId(model.Id));
            return response ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using OnlineExaminationSystems.UI.Helpers;
using OnlineExaminationSystems.UI.Models;

namespace OnlineExaminationSystems.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IApiRequestHelper _apiRequestHelper;

        public LoginController(IApiRequestHelper apiRequestHelper)
        {
            _apiRequestHelper = apiRequestHelper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var token = await _apiRequestHelper.PostAsync<string>(ApiEndpoints.LoginEndpoint, login);

            if (!string.IsNullOrEmpty(token))
            {
                return Json(token); 
            }

            return Unauthorized();
        }
    }
}

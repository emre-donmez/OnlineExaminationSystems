using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.UI.Helpers;
using OnlineExaminationSystems.UI.Models.Login;

namespace OnlineExaminationSystems.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IApiRequestHelper _apiRequestHelper;

        public AccountController(IApiRequestHelper apiRequestHelper)
        {
            _apiRequestHelper = apiRequestHelper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            SignOutAndClearCookies();
            var token = await _apiRequestHelper.PostAsync<string>(ApiEndpoints.LoginEndpoint, login);

            if (!string.IsNullOrEmpty(token))
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddMinutes(30)
                };

                Response.Cookies.Append("JWToken", token, cookieOptions);
                return RedirectToAction("Index", "Home");
            }

            return Unauthorized();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            SignOutAndClearCookies();
            return RedirectToAction("Index", "Account");
        }

        private async void SignOutAndClearCookies()
        {
            Response.Cookies.Delete("JWToken");
            await HttpContext.SignOutAsync();
        }
    }
}
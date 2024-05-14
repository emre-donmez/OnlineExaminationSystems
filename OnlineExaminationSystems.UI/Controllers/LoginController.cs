using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Login(Login login)
        {
            //Burada login işlemleri yapılacak ve token alınacak, sana kullanım örneği olsun diye yazdım
            //var result = await _apiRequestHelper.PostAsync<string>(ApiEndpoints.LoginEndpoint, login);
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace OnlineExaminationSystems.UI.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

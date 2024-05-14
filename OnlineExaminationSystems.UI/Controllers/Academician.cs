using Microsoft.AspNetCore.Mvc;

namespace OnlineExaminationSystems.UI.Controllers
{
    public class Academician : Controller
    {
        public IActionResult CreateQuestion()
        {
            return View();
        }
        public IActionResult Questions() {

            return View();
        }
    }
}

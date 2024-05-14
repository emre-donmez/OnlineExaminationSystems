using Microsoft.AspNetCore.Mvc;

namespace OnlineExaminationSystems.UI.Controllers
{
    public class ExamController : Controller
    {
        public IActionResult ExamPage()
        {
            return View();
        }
    }
}

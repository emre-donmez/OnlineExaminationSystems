using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.UI.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OnlineExaminationSystems.UI.Controllers
{
    public class ExamController : Controller
    {
        
       
        public async Task<IActionResult> Exams()
        {
            IEnumerable<Exam> exams = null;
            using(var client=new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7100/api/");
                //HTTP GET
                var result = await client.GetAsync("Exams");
         
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    var sd = JsonSerializer.Deserialize<List<Exam>>(readTask);
                }
                else
                {
                    exams = Enumerable.Empty<Exam>();
                    ModelState.AddModelError(string.Empty, "Server Error.");
                }

            }
            return View(exams);
        }
    }
}

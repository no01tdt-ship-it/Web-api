using Microsoft.AspNetCore.Mvc;

namespace Web_api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

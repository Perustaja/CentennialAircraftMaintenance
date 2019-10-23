using Microsoft.AspNetCore.Mvc;

namespace CAM.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

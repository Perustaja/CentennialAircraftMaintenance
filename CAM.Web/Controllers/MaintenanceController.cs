using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CAM.Web.Controllers
{
    public class MaintenanceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
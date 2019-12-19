using CAM.Core.Interfaces;
using CAM.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace CAM.Web.Controllers
{
    public class WorkOrdersController : Controller
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IDocumentGenerator _documentGenerator;
        public WorkOrdersController(ApplicationContext appContext, IDocumentGenerator docGen)
        {
            _applicationContext = appContext;
            _documentGenerator = docGen;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
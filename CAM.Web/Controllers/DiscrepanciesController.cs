using System;
using CAM.Core.Interfaces;
using CAM.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace CAM.Web.Controllers
{
    public class DiscrepanciesController : Controller
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IDocumentGenerator _documentGenerator;
        public DiscrepanciesController(ApplicationContext appContext, IDocumentGenerator docGen)
        {
            _applicationContext = appContext;
            _documentGenerator = docGen;
        }

        public IActionResult Index(string status, string regNum)
        {
            ViewData["Selection"] = (String.IsNullOrWhiteSpace(regNum)) ? "All Aircraft" : regNum;
            ViewData["Status"] = (String.IsNullOrWhiteSpace(status)) ? "Open" : status;
            
            
            return View();
        }
    }
}
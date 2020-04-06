using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CAM.Core.Interfaces;
using CAM.Core.Interfaces.Repositories;
using CAM.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace CAM.Web.Controllers
{
    public class WorkOrdersController : Controller
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IDocumentGenerator _documentGenerator;
        private readonly IMapper _mapper;

        public WorkOrdersController(
            ApplicationContext appContext,
            IDocumentGenerator docGen,
            IMapper mapper)
        {
            _applicationContext = appContext;
            _documentGenerator = docGen;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string status, string regNum)
        {
            return View();
        }
    }
}
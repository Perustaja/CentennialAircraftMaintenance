using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CAM.Core.Interfaces;
using CAM.Core.Interfaces.Repositories;
using CAM.Infrastructure.Data;
using CAM.Web.ViewModels.WorkOrders;
using Microsoft.AspNetCore.Mvc;

namespace CAM.Web.Controllers
{
    public class WorkOrdersController : Controller
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IDocumentGenerator _documentGenerator;
        private readonly IMapper _mapper;

        public WorkOrdersController(
            ApplicationContext appContext,
            IWorkOrderRepository workOrderRepository,
            IDocumentGenerator docGen,
            IMapper mapper)
        {
            _applicationContext = appContext;
            _documentGenerator = docGen;
            _workOrderRepository = workOrderRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string status, string regNum)
        {
            ViewData["RegistrationNum"] = (!String.IsNullOrWhiteSpace(regNum)) ? regNum : "";
            ViewData["WorkStatus"] = (!String.IsNullOrWhiteSpace(status)) ? status : "";

            var argStatus = status ?? "open";
            if (String.IsNullOrEmpty(regNum) || regNum == "All")
                regNum = "N";
            
            var discrepancies = await _workOrderRepository.GetBySearchParamsAsync(regNum, argStatus);

            var viewModel = _mapper.Map<List<WorkOrdersIndexViewModel>>(discrepancies);

            return View(viewModel);
        }
    }
}
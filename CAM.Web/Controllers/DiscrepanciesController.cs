using System;
using CAM.Core.Interfaces;
using CAM.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using CAM.Web.ViewModels.Discrepancies;
using CAM.Core.Interfaces.Repositories;

namespace CAM.Web.Controllers
{
    public class DiscrepanciesController : Controller
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IDiscrepancyRepository _discrepancyRepository;
        private readonly IDocumentGenerator _documentGenerator;

        private readonly IMapper _mapper;
        public DiscrepanciesController(
            ApplicationContext appContext,
            IDiscrepancyRepository discrepancyRepository,
            IDocumentGenerator docGen,
            IMapper mapper)
        {
            _applicationContext = appContext;
            _discrepancyRepository = discrepancyRepository;
            _documentGenerator = docGen;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string status, string regNum)
        {
            ViewData["RegistrationNum"] = (!String.IsNullOrWhiteSpace(regNum)) ? regNum : "";
            ViewData["WorkStatus"] = (!String.IsNullOrWhiteSpace(status)) ? status : "";

            var argStatus = status ?? "open";
            if (String.IsNullOrEmpty(regNum) || regNum == "All")
                regNum = "N";
            
            var discrepancies = await _discrepancyRepository.GetBySearchParamsAsync(regNum, argStatus);

            var viewModel = _mapper.Map<List<DiscrepanciesIndexViewModel>>(discrepancies);

            return View(viewModel);
        }

        public async Task<IActionResult> New()
        {
            return View();
        }
    }
}
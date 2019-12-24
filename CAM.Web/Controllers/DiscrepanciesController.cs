using System;
using CAM.Core.Interfaces;
using CAM.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using CAM.Infrastructure.Data.Queries;
using System.Threading.Tasks;
using System.Collections.Generic;
using CAM.Web.ViewModels;
using AutoMapper;
using CAM.Core.Entities;

namespace CAM.Web.Controllers
{
    public class DiscrepanciesController : Controller
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IDocumentGenerator _documentGenerator;
        private readonly IMapper _mapper;
        public DiscrepanciesController(
            ApplicationContext appContext, 
            IDocumentGenerator docGen,
            IMapper mapper)
        {
            _applicationContext = appContext;
            _documentGenerator = docGen;
            _mapper = mapper;
        }
        public async Task<IActionResult> IndexAsync(string status, string regNum)
        {
            ViewData["RegistrationNum"] = (!String.IsNullOrWhiteSpace(regNum)) ? regNum : "";
            ViewData["WorkStatus"] = (!String.IsNullOrWhiteSpace(status)) ? status : "";
            if (String.IsNullOrEmpty(status))
                status = "open";

            var discrepancies = await _applicationContext.Discrepancies.GetListAllAsync();

            var viewModel = _mapper.Map<List<DiscrepancyViewModel>>(discrepancies);

            return View(viewModel);
        }
    }
}
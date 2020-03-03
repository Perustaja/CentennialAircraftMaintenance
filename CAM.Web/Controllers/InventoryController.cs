using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using CAM.Core.Interfaces.Repositories;
using CAM.Core.Entities;
using CAM.Web.ViewModels.Inventory;
using CAM.Web.ViewModels.Shared;

namespace CAM.Web.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IPartRepository _partRepository;
        private readonly IMapper _mapper;
        public InventoryController(IPartRepository partRepository, IMapper mapper)
        {
            _partRepository = partRepository;
            _mapper = mapper;
        }
        [TempData]
        public string StatusMessage { get; set; }
        [TempData]
        public bool Success { get; set; } = false;
        public async Task<IActionResult> Index(string search, string filter)
        {
            ViewData["FilterValue"] = !String.IsNullOrEmpty(filter) ? filter : "";

            List<Part> parts;
            if (!String.IsNullOrEmpty(search) || !String.IsNullOrEmpty(filter))
                parts = await _partRepository.GetBySearchParamsAsync(search, filter, false);
            else
                parts = await _partRepository.GetListAllAsync(false);

            var viewmodel = _mapper.Map<List<InventoryIndexViewModel>>(parts);
            return View(viewmodel);
        }

        [HttpGet]
        public IActionResult Receive()
        {
            return View();
        }

        [HttpGet("receive/add")]
        public async Task<IActionResult> AddToList(string partId, int qty)
        {
            // fetch from db, verifying
            // return partial with viewmodel passed to it
            var part = _partRepository.GetByIdAsync(partId, false);
            if (part == null)
            {
                return Json(false);
            }
            var vm = _mapper.Map<InventoryReceiveListViewModel>(part);
            
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerifyPartExists(string inputPartNumber)
        {
            // just in case the input gets sent without being set
            if (String.IsNullOrWhiteSpace(inputPartNumber))
            {
                return Json($"The part number cannot be empty.");
            }
            else if (!await _partRepository.CheckForExistingRecordAsync(inputPartNumber))
            {
                return Json($"The part {inputPartNumber} could not be found.");
            }

            return Json(true);
        }
    }
}
using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using CAM.Core.Interfaces.Repositories;
using CAM.Core.Entities;
using CAM.Web.ViewModels.Inventory;
using CAM.Web.ViewModels.Shared;
using Microsoft.Extensions.Logging;

namespace CAM.Web.Controllers
{
    public class InventoryController : Controller
    {
        private readonly ILogger<InventoryController> _logger;
        private readonly IPartRepository _partRepository;
        private readonly IMapper _mapper;
        public InventoryController(ILogger<InventoryController> logger, IPartRepository partRepository, IMapper mapper)
        {
            _logger = logger;
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

        // Ajax only
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Receive(InventoryReceiveListViewModel vm)
        {
            
            if (!ModelState.IsValid)
            {
                return NotFound("test");
            }
            // validate EVERYTHING

            return Ok();
        }

        // Ajax only
        [HttpPost]
        public async Task<IActionResult> AddToReceivingList(InventoryReceiveViewModel vm)
        {
            if (vm != null && !String.IsNullOrWhiteSpace(vm.InputPartNumber) && vm.InputQuantity > 0)
            {
                var part = await _partRepository.GetByIdAsync(vm.InputPartNumber, false);
                if (part != null)
                {
                    var mappedVm = _mapper.Map<InventoryReceiveItemViewModel>(part);
                    mappedVm.ImgThumb = part.ImagePath; // temporary, needs thumbnail
                    mappedVm.Qty = vm.InputQuantity;
                    return PartialView("_ReceiveListPartial", mappedVm);
                }
            }
            return NotFound();
        }

        // Remote validation
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
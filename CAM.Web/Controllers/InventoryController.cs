using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using CAM.Core.Interfaces.Repositories;
using CAM.Web.ViewModels.Inventory;
using Microsoft.Extensions.Logging;
using CAM.Core.SharedKernel;
using CAM.Core.Interfaces;
using CAM.Web.ViewModels.Shared;
using CAM.Core.Entities;

namespace CAM.Web.Controllers
{
    public class InventoryController : Controller
    {
        private readonly ILogger<InventoryController> _logger;
        private readonly IPartRepository _partRepository;
        private readonly IMapper _mapper;
        private readonly IPaginatedListMapper _pListMapper;
        public InventoryController(ILogger<InventoryController> logger, IPartRepository partRepository,
        IMapper mapper, IPaginatedListMapper pListMapper)
        {
            _logger = logger;
            _partRepository = partRepository;
            _mapper = mapper;
            _pListMapper = pListMapper;
        }

        [TempData]
        public string StatusMessage { get; set; }
        [TempData]
        public bool Success { get; set; } = false;

        public async Task<IActionResult> Index(string search, string filter, int page = 1, int ipp = 10)
        {
            ViewData["FilterValue"] = !String.IsNullOrWhiteSpace(filter) ? filter : String.Empty;
            ViewData["SearchValue"] = !String.IsNullOrWhiteSpace(search) ? search : String.Empty;
            ViewData["PageValue"] = page;
            ViewData["IppValue"] = ipp;

            var parts = await _partRepository.GetBySearchParamsAsync(search, filter, page, ipp, false);

            var viewModel = new InventoryIndexViewModel()
            {
                PaginatedParts = _pListMapper.MapToViewModelList<PartViewModel, Part>(parts)
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Receive()
        {
            return View();
        }

        // Ajax
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnReceivePost(InventoryReceiveListViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                return NotFound("test");
            }
            // validate EVERYTHING

            return Ok();
        }

        // Ajax
        [HttpPost]
        public async Task<IActionResult> OnAddToReceivingListPost(InventoryReceiveViewModel vm)
        {
            if (vm != null && !String.IsNullOrWhiteSpace(vm.InputPartNumber) && vm.InputQuantity > 0)
            {
                var part = await _partRepository.GetByIdAsync(vm.InputPartNumber, false);
                if (part != null)
                {
                    var mappedVm = _mapper.Map<InventoryReceiveItemViewModel>(part);
                    mappedVm.Qty = vm.InputQuantity;
                    return PartialView("_ReceiveListPartial", mappedVm);
                }
            }
            return NotFound();
        }

        // Remote validation
        [AcceptVerbs("GET")]
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

        [AcceptVerbs("GET")]
        public async Task<IActionResult> VerifyPartNonExistent(string id)
        {
            if (!String.IsNullOrWhiteSpace(id) && await _partRepository.CheckForExistingRecordAsync(id))
            {
                return Json($"A part already exists with the part number {id}.");
            }
            return Json(true);
        }
    }
}
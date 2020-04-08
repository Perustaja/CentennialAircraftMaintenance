using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using CAM.Web.ViewModels.Inventory;
using CAM.Core.Interfaces;
using CAM.Web.ViewModels.Shared;
using CAM.Core.Entities;
using CAM.Core.Interfaces.Services;
using System.Linq;

namespace CAM.Web.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IPartsService _partsService;
        private readonly IMapper _mapper;
        private readonly IPaginatedListMapper _pListMapper;
        public InventoryController(IPartsService partsService, IMapper mapper, IPaginatedListMapper pListMapper)
        {
            _partsService = partsService;
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

            var parts = await _partsService.GetPaginatedPartsBySearchParams(search, filter, page, ipp);

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
            if (ModelState.IsValid)
            {
                var ids = vm.ReceiveItems.Select(i => i.Id).ToList();
                var qtys = vm.ReceiveItems.Select(i => i.Qty).ToList();
                if (await _partsService.TryReceiveShipment(ids, qtys))
                    return Ok();
            }
            return NotFound("There was an error handling your request. Please try again and if the problem persists, contact site administation.");
        }

        // Ajax
        [HttpPost]
        public async Task<IActionResult> OnAddToReceivingListPost(InventoryReceiveViewModel vm)
        {
            if (!String.IsNullOrWhiteSpace(vm.InputPartNumber) && vm.InputQuantity > 0)
            {
                var part = await _partsService.GetPartOrDefaultByMfrsPartNumber(vm.InputPartNumber, false);
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
            else if (!await _partsService.PartExists(inputPartNumber))
            {
                return Json($"The part {inputPartNumber} could not be found.");
            }
            return Json(true);
        }

        [AcceptVerbs("GET")]
        public async Task<IActionResult> VerifyPartNonExistent(string id)
        {
            if (!String.IsNullOrWhiteSpace(id) && await _partsService.PartExists(id))
            {
                return Json($"A part already exists with the manufacturer's part number {id}.");
            }
            return Json(true);
        }
    }
}
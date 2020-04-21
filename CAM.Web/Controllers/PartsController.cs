using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using CAM.Web.ViewModels.Parts;
using CAM.Core.Interfaces.Services;

namespace CAM.Web.Controllers
{
    [Route("inventory/p", Name = "Parts")]
    public class PartsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPartsService _partsService;
        public PartsController(IMapper mapper, IPartsService partsService)
        {
            _mapper = mapper;
            _partsService = partsService;
        }

        [TempData]
        public string StatusMessage { get; set; }
        [TempData]
        public bool Success { get; set; } = false;

        // details
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var part = await _partsService.GetPartOrDefaultById(id, false);
            if (part == null)
            {
                return new BadRequestResult();
            }
            var viewmodel = _mapper.Map<PartsDetailsViewModel>(part);

            return View(viewmodel);
        }

        // edit GET
        [HttpGet("edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var part = await _partsService.GetPartOrDefaultById(id, false);
            if (part == null)
            {
                StatusMessage = "Unable to locate a matching part.";
                Success = false;
                return RedirectToAction("Index", "Inventory");
            }
            var viewmodel = _mapper.Map<PartsEditViewModel>(part);

            return View(viewmodel);
        }

        // edit POST
        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PartsEditViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var editSuccessful = (await _partsService.TryEditPart(vm.Id, vm.MfrsPartNumber, vm.PartCategoryId, vm.CataloguePartNumber,
            vm.Name, vm.Description, vm.PriceIn, vm.PriceOut, vm.Vendor, vm.MinimumStock, vm.Image));

            if (editSuccessful)
            {
                StatusMessage = ("Changes saved successfully.");
                Success = true;
            }
            else
            {
                StatusMessage = "There was an error handling your request. Try again, and if the issue persists contact site administration.";
                Success = false;
            }
            return RedirectToAction("Index", "Inventory");
        }

        // delete (handled by modal)
        [HttpPost("delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _partsService.TryDeletePart(id))
            {
                StatusMessage = $"Part was successfully deleted.";
                Success = true;
            }
            else
            {
                StatusMessage = "There was an error handling your request. Try again, and if the issue persists contact site administration.";
                Success = false;
            }
            return RedirectToAction("Index", "Inventory");
        }

        // Ajax
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnCreatePost(PartsCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The image specified does not follow guidelines. Please ensure the file has a valid extension and size.");
            }
            var createSuccessful = await _partsService.TryCreatePart(vm.MfrsPartNumber, vm.PartCategoryId, vm.CataloguePartNumber, vm.Name, vm.Description,
            vm.PriceIn, vm.PriceOut, vm.Vendor, vm.MinimumStock, vm.Image);

            if (createSuccessful)
                return CreatedAtAction("Parts", vm.MfrsPartNumber);
            else
                return BadRequest("Unable to add the specified part. Please try again and contact site administration if the problem persists.");
        }
    }
}
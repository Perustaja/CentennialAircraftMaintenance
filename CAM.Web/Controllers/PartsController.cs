using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using CAM.Core.Interfaces.Repositories;
using CAM.Core.Entities;
using CAM.Web.ViewModels.Parts;
using CAM.Core.Interfaces;
using System;
using Microsoft.Extensions.Logging;

namespace CAM.Web.Controllers
{
    [Route("inventory/p", Name = "Parts")]
    public class PartsController : Controller
    {
        private readonly IPartRepository _partRepository;
        private readonly IMapper _mapper;
        private readonly IFileHandler _fileHandler;
        private readonly ILogger<PartsController> _logger;
        public PartsController(IPartRepository partRepository, IMapper mapper, IFileHandler fileHandler, ILogger<PartsController> logger)
        {
            _partRepository = partRepository;
            _mapper = mapper;
            _fileHandler = fileHandler;
            _logger = logger;
        }

        [TempData]
        public string StatusMessage { get; set; }
        [TempData]
        public bool Success { get; set; } = false;

        // details
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            // null id won't be encountered
            var part = await _partRepository.GetByIdAsync(id, false);
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
            var part = await _partRepository.GetByIdAsync(id, false);
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
            // Assume current values are valid
            var part = await _partRepository.GetByIdAsync(vm.Id);
            if (part == null)
            {
                StatusMessage = "Unable to locate this part. Please try again.";
                Success = false;
            }
            // if a new image was provided, try to save it and get the filepath.
            try
            {
                part.EditPart(vm.MfrsPartNumber, vm.PartCategoryId, vm.CataloguePartNumber, 
                vm.Name, vm.Description, vm.PriceIn, vm.PriceOut, vm.Vendor, vm.MinimumStock);
                part = await _fileHandler.SetPartImage(part, vm.Image);

                await _partRepository.SaveChangesAsync();
                StatusMessage = ("Changes saved successfully.");
                Success = true;
                return RedirectToAction("Index", "Inventory");
            }
            catch (Exception)
            {
                StatusMessage = "There was an error handling your request. Try again, and if the issue persists contact site administration.";
                _logger.LogCritical($"{DateTime.Now}: Exception when trying to edit existing part.");
                Success = false;
            }

            return RedirectToAction("Index", "Inventory");
        }

        // delete (handled by modal)
        [HttpPost("delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var part = await _partRepository.GetByIdAsync(id);
            if (part == null)
            {
                StatusMessage = "Unable to locate this part. Please try again.";
                Success = false;
            }
            try
            {
                await _partRepository.DeleteAsync(part);
                StatusMessage = $"Part \"{part.MfrsPartNumber}\" was successfully deleted.";
                Success = true;
            }
            catch (Exception)
            {
                StatusMessage = "There was an error handling your request. Try again, and if the issue persists contact site administration.";
                _logger.LogCritical($"{DateTime.Now}: Exception when trying to delete part: {part.Id}.");
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
            try
            {
                var part = new Part(vm.MfrsPartNumber, vm.PartCategoryId, vm.CataloguePartNumber, vm.Name, vm.Description,
                vm.PriceIn, vm.PriceOut, vm.Vendor, vm.MinimumStock);
                
                await _partRepository.AddAsync(part);
                part = await _fileHandler.SetPartImage(part, vm.Image);
                await _partRepository.SaveChangesAsync();

                return CreatedAtAction("Parts", vm.MfrsPartNumber);
            }
            catch (Exception)
            {
                _logger.LogCritical($"{DateTime.Now}: Exception when trying to save new part {vm.MfrsPartNumber}.");
            }
            return BadRequest("Unable to add the specified part. Please try again and contact site administration if the problem persists.");
        }
    }
}
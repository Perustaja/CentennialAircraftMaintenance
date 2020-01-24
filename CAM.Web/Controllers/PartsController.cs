using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using CAM.Core.Interfaces.Repositories;
using CAM.Core.Entities;
using CAM.Web.ViewModels.Parts;
using Microsoft.EntityFrameworkCore;
using CAM.Core.SharedKernel;
using System.IO;
using CAM.Core.Interfaces;
using System;
using CAM.Web.ViewModels.Shared;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(string id)
        {
            var part = await _partRepository.GetByIdAsync(id, false);
            var viewmodel = _mapper.Map<PartsDetailsViewModel>(part);

            return View(viewmodel);
        }

        [HttpGet("new")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PartsCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            string filePath = await _fileHandler.TrySaveImageAndReturnPathAsync(vm.Id, vm.Image, Constants.PARTS_DIRECTORY);

            var part = new Part(vm.Id, vm.PartCategoryId, vm.CataloguePartNumber, vm.Name, vm.Description,
            filePath, vm.PriceIn, vm.PriceOut, vm.Vendor, vm.MinimumStock);
            try
            {
                if (await _partRepository.CheckForExistingRecordAsync(vm.Id))
                {
                    ModelState.AddModelError(String.Empty, "A part already exists with this manufacturer's part number");
                }
                else
                {
                    await _partRepository.AddAsync(part);
                    StatusMessage = ("Your new part has been successfully saved.");
                    Success = true;
                    return RedirectToAction("Index", "Inventory");
                }
            }
            catch (Exception exc)
            {
                StatusMessage = "There was an error handling your request. Try again, and if the issue persists contact site administration.";
                _logger.LogCritical(exc, $"{DateTime.Now}: Exception when trying to save new part {part.Id}. {exc}");
                Success = false;
            }

            return View();
        }
        // edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var part = await _partRepository.GetByIdAsync(id, false);
            if (part == null)
            {
                StatusMessage = "Unable to locate this part. Please try again.";
                Success = false;
            }

            try
            {
                await _partRepository.DeleteAsync(part);
                StatusMessage = $"Part \"{part.Id}\" was successfully deleted.";
                Success = true;
            }
            catch (Exception exc)
            {
                StatusMessage = "There was an error handling your request. Try again, and if the issue persists contact site administration.";
                _logger.LogCritical(exc, $"{DateTime.Now}: Exception when trying to delete part {part.Id}. {exc}");
                Success = false;
            }

            return RedirectToAction("Index", "Inventory");
        }
    }
}
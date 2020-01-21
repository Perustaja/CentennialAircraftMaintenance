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

namespace CAM.Web.Controllers
{
    [Route("inventory/p")]
    public class PartsController : Controller
    {
        private readonly IPartRepository _partRepository;
        private readonly IMapper _mapper;
        private readonly IFileHandler _fileHandler;
        public PartsController(IPartRepository partRepository, IMapper mapper, IFileHandler fileHandler)
        {
            _partRepository = partRepository;
            _mapper = mapper;
            _fileHandler = fileHandler;
        }

        [TempData]
        public string SuccessMessage { get; set; }

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
                    SuccessMessage = "New part successfuly saved.";
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(String.Empty, "Error saving changes. Try again, if the problem persists contact site administration.");
            }
            return View();
        }
    }
}
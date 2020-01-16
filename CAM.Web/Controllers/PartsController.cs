using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using CAM.Core.Interfaces.Repositories;
using CAM.Core.Entities;
using CAM.Web.ViewModels.Parts;
using Microsoft.EntityFrameworkCore;
using CAM.Core.SharedKernel;
using System.IO;

namespace CAM.Web.Controllers
{
    [Route("inventory/p")]
    public class PartsController : Controller
    {
        private readonly IPartRepository _partRepository;
        private readonly IMapper _mapper;
        public PartsController(IPartRepository partRepository, IMapper mapper)
        {
            _partRepository = partRepository;
            _mapper = mapper;
        }

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
            // create the file path
            string filePath;
            if (vm.Image == null)
            {
                filePath = $"{Constants.PARTS_DIRECTORY}/default.png";
            }
            else
            {
                filePath = $"{Constants.PARTS_DIRECTORY}/{vm.Id.ToUpper()}{Path.GetExtension(vm.Image.FileName).ToLowerInvariant()}";
            }
            // save the formfile to the path
            using (var stream = System.IO.File.Create(filePath))
            {
                await vm.Image.CopyToAsync(stream);
            }

            var part = new Part(vm.Id, vm.PartCategoryId, vm.CataloguePartNumber, vm.Name, vm.Description,
            filePath, vm.PriceIn, vm.PriceOut, vm.Vendor, vm.MinimumStock);
            try
            {
                if (ModelState.IsValid)
                {
                    await _partRepository.AddAsync(part);
                    // return some modelstate message?
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Error saving changes. Try again, if the problem persists contact site administration.");
            }
            return View(vm);
        }

    }
}
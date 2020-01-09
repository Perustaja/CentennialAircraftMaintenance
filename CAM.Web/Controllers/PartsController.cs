using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using CAM.Core.Interfaces.Repositories;
using CAM.Core.Entities;
using CAM.Web.ViewModels.Inventory;
using CAM.Web.ViewModels.Parts;

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

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            return View();
        }
    }
}
using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using CAM.Core.Interfaces.Repositories;
using CAM.Core.Entities;
using CAM.Web.ViewModels.Inventory;

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
    }
}
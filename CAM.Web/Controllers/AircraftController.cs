using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CAM.Infrastructure.Data;
using CAM.Core.Entities;
using CAM.Core.Interfaces.Repositories;

namespace CAM.Web.Controllers
{
    public class AircraftController : Controller
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IAircraftRepository _aircraftRepository;
        public AircraftController(
            ApplicationContext applicationContext, 
            IAircraftRepository aircraftRepository)
        {
            _applicationContext = applicationContext;
            _aircraftRepository = aircraftRepository;
        }
        
        // GET aicraft/index/searchReg?
        public async Task<IActionResult> Index(string searchReg)
        {
            var aircraft = new List<Aircraft>();

            if (!String.IsNullOrEmpty(searchReg))
            {
                aircraft = await _aircraftRepository.GetListAsync(searchReg.ToUpper());
            }
            else
            {
                aircraft = await _aircraftRepository.GetListAllAsync();
            }
            return View(aircraft);
        }
        // GET aircraft/details/id
        public async Task<IActionResult> Details(string id)
        {
            if (id == null) {return NotFound();}

            var aircraft = await _aircraftRepository.GetByIdAsync(id);

            if (aircraft == null) {return NotFound();}
            return View(aircraft);
        }
        // GET aircraft/edit/id
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) {return NotFound();}

            var aircraft = await _aircraftRepository.GetByIdAsync(id);

            if (aircraft == null) {return NotFound();}
            return View(aircraft);
        }

    }
}
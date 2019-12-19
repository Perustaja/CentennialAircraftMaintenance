using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CAM.Infrastructure.Data;
using CAM.Infrastructure.Data.DbSetExtensions;
using CAM.Core.Entities;

namespace CAM.Web.Controllers
{
    public class AircraftController : Controller
    {
        private readonly ApplicationContext _applicationContext;
        public AircraftController(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        
        // GET aicraft/index/searchReg?
        public async Task<IActionResult> Index(string searchReg)
        {
            var aircraft = new List<Aircraft>();

            if (!String.IsNullOrEmpty(searchReg))
            {
                aircraft = await _applicationContext.Aircraft.GetListAsync(searchReg.ToUpper());
            }
            else
            {
                aircraft = await _applicationContext.Aircraft.GetListAllAsync();
            }
            return View(aircraft);
        }
        // GET aircraft/details/id
        public async Task<IActionResult> Details(string id)
        {
            if (id == null) {return NotFound();}

            var aircraft = await _applicationContext.Aircraft.GetByIdAsync(id);

            if (aircraft == null) {return NotFound();}
            return View(aircraft);
        }
        // GET aircraft/edit/id
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) {return NotFound();}

            var aircraft = await _applicationContext.Aircraft.GetByIdAsync(id);

            if (aircraft == null) {return NotFound();}
            return View(aircraft);
        }

    }
}
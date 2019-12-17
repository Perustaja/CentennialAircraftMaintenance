using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CAM.Infrastructure.Data;
using CAM.Infrastructure.Data.DbSetExtensions;

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
            var aircraft = from a in _applicationContext.Aircraft
                            select a;
            if (!String.IsNullOrEmpty(searchReg))
            {
                aircraft = aircraft.Where(a => a.Id.Contains(searchReg.ToUpper()));
            }
            return View(await aircraft.AsNoTracking().ToListAsync());
        }
        // GET aircraft/details/id
        public async Task<IActionResult> Details(string id)
        {
            // null check on id
            if (id == null) {return NotFound();}
            // grab aircraft with id matching id arg
            var aircraft = await _applicationContext.Aircraft.GetByIdAsync(id);
            // null check the grabbed aircraft, if id is non-existent it will be default(null)
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
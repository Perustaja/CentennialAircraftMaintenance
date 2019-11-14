using System;
using System.Threading.Tasks;
using System.Linq;
using CAM.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace CAM.Web.Controllers
{
    public class AircraftController : Controller
    {
        private readonly ApplicationContext _context;
        public AircraftController(ApplicationContext context)
        {
            _context = context;
        }

        // GET aircraft/index/{searchReg?}
        public async Task<IActionResult> Index(string searchReg)
        {
            return View(await _context.Aircraft.ToListAsync());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CAM.Core.Entities;
using CAM.Infrastructure.Data;
using AutoMapper;
using CAM.Web.ApiModels;

namespace CAM.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public AircraftController(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Aircraft
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AircraftDto>>> GetAircraft()
        {
            var aircraft = await _context.Aircraft.ToListAsync();
            return _mapper.Map<List<Aircraft>, List<AircraftDto>>(aircraft);
        }

        // GET: api/Aircraft/N12345
        [HttpGet("{id}")]
        public async Task<ActionResult<Aircraft>> GetAircraft(string id)
        {
            var aircraft = await _context.Aircraft.FindAsync(id);

            if (aircraft == null)
            {
                return NotFound();
            }

            return aircraft;
        }

        // PUT: api/Aircraft/N12345
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAircraft(string id, Aircraft aircraft)
        {
            if (id != aircraft.Id)
            {
                return BadRequest();
            }

            _context.Entry(aircraft).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AircraftExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Aircraft
        [HttpPost]
        public async Task<ActionResult<Aircraft>> PostAircraft(Aircraft aircraft)
        {
            _context.Aircraft.Add(aircraft);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAircraft", new { id = aircraft.Id }, aircraft);
        }

        private bool AircraftExists(string id)
        {
            return _context.Aircraft.Any(e => e.Id == id);
        }
    }
}

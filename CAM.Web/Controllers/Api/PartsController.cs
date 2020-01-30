using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CAM.Core.Entities;
using CAM.Core.Interfaces.Repositories;
using CAM.Web.ApiModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CAM.Web.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PartsController : ControllerBase
    {
        private readonly IPartRepository _partRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PartsController> _logger;
        public PartsController(IPartRepository partRepository, IMapper mapper, ILogger<PartsController> logger)
        {
            _partRepository = partRepository;
            _mapper = mapper;
            _logger = logger;
        }
        // GET api/parts/search?part-number=....
        [HttpGet("{part-number}")]
        public async Task<ActionResult<List<PartDto>>> Search(string part_number)
        {
            // get the part, passing String.Empty to note that there is no filter.
            var parts = await _partRepository.GetBySearchParamsAsync(part_number, string.Empty, false);
            if (parts.Count == 0)
            {
                return NotFound();
            }
            var partDtos = _mapper.Map<List<PartDto>>(parts);

            return partDtos;
        }
    }
}
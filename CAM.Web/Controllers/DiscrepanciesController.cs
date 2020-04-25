using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CAM.Core.Interfaces.Services;
using CAM.Web.ViewModels.Discrepancies;
using CAM.Web.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;

namespace CAM.Web.Controllers
{
    [Route("work-orders/{wOrderId}/discrepancies", Name = "Discrepancies")]
    public class DiscrepanciesController : Controller
    {
        private readonly IDiscrepancyService _discrepService;
        private readonly IMapper _mapper;
        public DiscrepanciesController(IDiscrepancyService discrepService, IMapper mapper)
        {
            _discrepService = discrepService;
            _mapper = mapper;
        }
        // Returns the nth discrepancy in the workorder
        [HttpGet("{index}")]
        public async Task<IActionResult> Details(int wOrderId, int index)
        {
            var discrep = await _discrepService.GetDiscrepOrDefaultByIndex(wOrderId, index, false);
            if (discrep == null)
                return NotFound();

            var vm = _mapper.Map<DiscrepanciesDetailsViewModel>(discrep);
            return View(vm);
        }
        // /{id}/history
        // Ajax
        [HttpPost("/onaddpart")]
        public async Task<IActionResult> OnAddPart(DiscrepanciesAddPartViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                if (await _discrepService.TryAddPart(vm.DiscrepancyId, vm.PartId, vm.InputQuantity))
                {
                    var viewModels = new List<DiscrepancyPartViewModel>();
                    var updatedParts = await _discrepService.GetDiscrepancyPartsById(vm.DiscrepancyId);
                    updatedParts.ForEach(dp => viewModels.Add(new DiscrepancyPartViewModel()
                    {
                        PartId = dp.PartId,
                        DiscrepancyId = dp.DiscrepancyId,
                        MfrsPartNumber = dp.Part.MfrsPartNumber,
                        Name = dp.Part.Name,
                        ImageThumbPath = dp.Part.ImageThumbPath,
                        Qty = dp.Qty
                    }));
                    return PartialView("_DiscrepancyPartsPartial", viewModels);
                }
            }
            return NotFound("Unable to locate either the part or discrepancy requested.");
        }
        // Ajax
        [HttpPost("/onaddlabor")]
        public async Task<IActionResult> OnAddLabor(DiscrepanciesAddLaborViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                if (await _discrepService.TryAddLabor(vm.DiscrepancyId, vm.EmployeeId, vm.LaborInHours, vm.DatePerformed))
                {
                    var updatedRecords = await _discrepService.GetLaborRecordsById(vm.DiscrepancyId);
                    var viewModels = _mapper.Map<List<LaborRecordViewModel>>(updatedRecords);
                    return PartialView("_DiscrepancyPartsPartial", viewModels);
                }
            }
            return NotFound("Unable to add the requested labor record.");
        }
    }
}
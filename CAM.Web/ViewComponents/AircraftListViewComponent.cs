using CAM.Infrastructure.Data;
using CAM.Web.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Aspen.Views.Squawks.Components
{
    public class AircraftListViewComponent : ViewComponent
    {
        private readonly ApplicationContext _applicationContext;
        public AircraftListViewComponent(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            AircraftListViewModel aircraftListViewModel = new AircraftListViewModel
            {
                Ids = await _applicationContext.Aircraft.Select(a => a.Id).ToListAsync()
            };
            return View(aircraftListViewModel);
        }
    }
}
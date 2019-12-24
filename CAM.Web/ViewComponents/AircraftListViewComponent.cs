using CAM.Infrastructure.Data;
using CAM.Infrastructure.Data.Queries;
using CAM.Web.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CAM.Web.ViewComponents
{
    public class AircraftListViewComponent : ViewComponent
    {
        private readonly ApplicationContext _applicationContext;
        public AircraftListViewComponent(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public async Task<IViewComponentResult> InvokeAsync(string selected)
        {
            var aircraftListViewModel = new AircraftListViewModel()
            {
                Ids = new List<string>(),
                Selected = new List<bool>()
            };

            foreach (var aircraft in await _applicationContext.Aircraft.GetListAllAsync())
            {
                aircraftListViewModel.Ids.Add(aircraft.Id);
                aircraftListViewModel.Selected.Add(aircraft.Id == selected);
            }
            
            return View(aircraftListViewModel);
        }
    }
}
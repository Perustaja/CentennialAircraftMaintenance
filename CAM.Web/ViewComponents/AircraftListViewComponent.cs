using CAM.Core.Entities;
using CAM.Core.Interfaces.Repositories;
using CAM.Infrastructure.Data;
using CAM.Web.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CAM.Web.ViewComponents
{
    public class AircraftListViewComponent : ViewComponent
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IAircraftRepository _aircraftRepository;
        public AircraftListViewComponent(
            ApplicationContext applicationContext, 
            IAircraftRepository aircraftRepository)
        {
            _applicationContext = applicationContext;
            _aircraftRepository = aircraftRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(string selected)
        {
            var aircraftListViewModel = new AircraftListViewModel()
            {
                Ids = new List<string>(),
                Selected = new List<bool>()
            };

            foreach (var aircraft in await _aircraftRepository.GetListAllAsync(false))
            {
                aircraftListViewModel.Ids.Add(aircraft.Id);
                aircraftListViewModel.Selected.Add(aircraft.Id == selected);
            }
            
            return View(aircraftListViewModel);
        }
    }
}
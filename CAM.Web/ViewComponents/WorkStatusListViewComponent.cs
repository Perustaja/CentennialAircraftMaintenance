using CAM.Core.Entities;
using CAM.Core.Interfaces.Repositories;
using CAM.Infrastructure.Data;
using CAM.Web.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CAM.Web.ViewComponents
{
    public class WorkStatusListViewComponent : ViewComponent
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IAsyncRepository<WorkStatus, int> _genericRepository;
        public WorkStatusListViewComponent(
            ApplicationContext applicationContext,
            IAsyncRepository<WorkStatus, int> genericRepository)
        {
            _applicationContext = applicationContext;
            _genericRepository = genericRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(string selected)
        {
            var workStatusListViewModel = new WorkStatusListViewModel()
            {
                Descriptions = new List<string>(),
                Selected = new List<bool>()
            };

            foreach (var status in await _genericRepository.GenericListAllAsync())
            {
                workStatusListViewModel.Descriptions.Add(status.Description);
                workStatusListViewModel.Selected.Add(status.Description.ToLower() == selected);
            }
            
            return View(workStatusListViewModel);
        }
    }
}
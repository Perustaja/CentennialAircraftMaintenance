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
        private readonly IWorkStatusRepository _workStatusRepository;
        public WorkStatusListViewComponent(
            ApplicationContext applicationContext,
            IWorkStatusRepository workStatusRepository)
        {
            _applicationContext = applicationContext;
            _workStatusRepository = workStatusRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(string selected)
        {
            var workStatusListViewModel = new WorkStatusListViewModel()
            {
                Descriptions = new List<string>(),
                Selected = new List<bool>()
            };

            foreach (var status in await _workStatusRepository.GetListAllAsync())
            {
                workStatusListViewModel.Descriptions.Add(status.Description);
                workStatusListViewModel.Selected.Add(status.Description.ToLower() == selected);
            }
            
            return View(workStatusListViewModel);
        }
    }
}
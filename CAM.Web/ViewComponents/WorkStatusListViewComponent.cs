using CAM.Core.Interfaces.Repositories;
using CAM.Web.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CAM.Web.ViewComponents
{
    public class WorkStatusListViewComponent : ViewComponent
    {
        private readonly IWorkStatusRepository _workStatusRepository;
        public WorkStatusListViewComponent(IWorkStatusRepository workStatusRepository)
        {
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
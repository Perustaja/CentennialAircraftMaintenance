using CAM.Infrastructure.Data;
using CAM.Infrastructure.Data.Queries;
using CAM.Web.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CAM.Web.ViewComponents
{
    public class WorkStatusListViewComponent : ViewComponent
    {
        private readonly ApplicationContext _applicationContext;
        public WorkStatusListViewComponent(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public async Task<IViewComponentResult> InvokeAsync(string selected)
        {
            var workStatusListViewModel = new WorkStatusListViewModel()
            {
                Descriptions = new List<string>(),
                Selected = new List<bool>()
            };

            foreach (var status in await _applicationContext.WorkStatuses.GetListAllAsync())
            {
                workStatusListViewModel.Descriptions.Add(status.Description);
                workStatusListViewModel.Selected.Add(status.Description.ToLower() == selected);
            }
            
            return View(workStatusListViewModel);
        }
    }
}
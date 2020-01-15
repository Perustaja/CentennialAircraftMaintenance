using System.Collections.Generic;
using System.Threading.Tasks;
using CAM.Core.Interfaces.Repositories;
using CAM.Web.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;

namespace CAM.Web.ViewComponents
{
    public class PartCategoryListViewComponent : ViewComponent
    {
        private readonly IPartCategoryRepository _partCategoryRepository;
        public PartCategoryListViewComponent(IPartCategoryRepository workStatusRepository)
        {
            _partCategoryRepository = workStatusRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var viewmodel = new PartCategoryListViewModel()
            {
                Ids = new List<int>(),
                Names = new List<string>()
            };

            foreach (var cat in await _partCategoryRepository.GetListAllAsync())
            {
                viewmodel.Ids.Add(cat.Id);
                viewmodel.Names.Add(cat.Name);
            }

            return View(viewmodel);
        }
    }
}
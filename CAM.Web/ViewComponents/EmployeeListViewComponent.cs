using AutoMapper;
using CAM.Core.Interfaces.Repositories;
using CAM.Web.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CAM.Web.ViewComponents
{
    public class EmployeeListViewComponent : ViewComponent
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeListViewComponent(IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(string selected)
        {
            var employees = await _employeeRepository.GetListAll();
            var viewModels = _mapper.Map<List<EmployeeViewModel>>(employees);
            return View(viewModels);
        }
    }
}
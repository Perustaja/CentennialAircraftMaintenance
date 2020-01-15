using System.Collections.Generic;
using AutoMapper;
using CAM.Core.Entities;

namespace CAM.Web.ViewModels.Shared
{
    public class PartCategoryListViewModel
    {
        public List<int> Ids { get ; set; }
        public List<string> Names { get; set; }
    }
}
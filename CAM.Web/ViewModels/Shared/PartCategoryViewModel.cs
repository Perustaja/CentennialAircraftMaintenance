using System.Collections.Generic;
using AutoMapper;
using CAM.Core.Entities;

namespace CAM.Web.ViewModels.Shared
{
    [AutoMap(typeof(PartCategory), ReverseMap = true)]
    public class PartCategoryViewModel
    {
        public string Name { get; set; }
    }
}
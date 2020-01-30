using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CAM.Core.Entities;

namespace CAM.Web.ApiModels
{
    [AutoMap(typeof(PartCategory), ReverseMap = true)]
    public class PartCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
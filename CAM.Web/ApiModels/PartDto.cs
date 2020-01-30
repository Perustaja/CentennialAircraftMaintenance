using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using CAM.Core.SharedKernel;
using CAM.Core.Entities;
using CAM.Core.Entities.DiscrepancyAggregate;
using AutoMapper;

namespace CAM.Web.ApiModels
{
    /// <summary>
    /// Contains general item information used for inventory purposes. 
    /// </summary>
    [AutoMap(typeof(Part), ReverseMap = true)]
    public class PartDto
    {
        public string Id { get; set; }
        // Main
        public string CataloguePartNumber { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int CurrentStock { get; set; }
        public int QtySoldToDate { get; set; }
        public decimal PriceIn { get; set; }
        public decimal? PriceOut { get; set; }
        public string Vendor { get; set; }
        public bool IsDiscontinued { get; set; } = false;
        public int? MinimumStock { get; set; }
        // Category 
        public PartCategoryDto PartCategory { get; set; }
    }
}
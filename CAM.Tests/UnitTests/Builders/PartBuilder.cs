using System;
using System.Collections.Generic;
using CAM.Core.Entities;
using CAM.Web.ViewModels.Parts;
using Microsoft.AspNetCore.Http;

namespace CAM.Tests.Builders
{
    public class PartBuilder
    {
        public static PartsCreateViewModel ReturnValidPartsCreateViewModel()
        {
            return new PartsCreateViewModel()
            {
                Id = "test",
                PartCategoryId = 1,
                CataloguePartNumber = "test",
                Name = "test",
                Description = "test",
                PriceIn = 1m,
                PriceOut = 1m,
                Vendor = "test",
                MinimumStock = 0,
                Image = null
            };
        }
    }
}
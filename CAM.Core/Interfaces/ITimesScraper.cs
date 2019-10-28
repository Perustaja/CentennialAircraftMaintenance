using System.Collections.Generic;
using CAM.Core.Entities;
using CAM.Core.Options;

namespace CAM.Core.Interfaces
{
    public interface ITimesScraper
    {
        ISet<Times> Run(FspScraperOptions options);
    }
}
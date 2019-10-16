using System.Collections.Generic;
using CAM.Core.Entities;

namespace CAM.Core.Interfaces
{
    public interface ITimesScraper
    {
        ISet<Times> Run();
    }
}
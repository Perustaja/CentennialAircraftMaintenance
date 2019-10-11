using System.Collections.Generic;
using OpenQA.Selenium;
using CAM.Core.Entities;
using OpenQA.Selenium.Support.UI;

namespace CAM.Core.Interfaces
{
    public interface ITimesScraper
    {
        ISet<Times> Run();
    }
}
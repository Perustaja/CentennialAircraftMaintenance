using System.Reflection;
using System.Reflection.Emit;
using System.Linq;
using System.Collections.Generic;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAM.Core.Entities;
using CAM.Core.Interfaces;
using Microsoft.Extensions.Options;
using CAM.Core.Options;

namespace CAM.Infrastructure.Services.TimesScraper
{
    /// <summary>
    /// Contains methods used for scraping times
    /// </summary>
    public class FspTimesScraper : ITimesScraper
    {
        /// <summary>
        /// Initializes a ChromeDriver and then navigates to the desired page, scrapes, parses, and returns an ISet of the entity.
        /// </summary>
        public ISet<Times> Run(FspScraperOptions options)
        {
            IWebDriver driver = new ChromeDriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            Login(driver, options);
            var times = ScrapeTimes(driver, wait);
            driver.Quit();
            return times;
        }
        /// <summary>
        /// Logs in using the stored username and password, and then navigates to the desired url.
        /// </summary>
        public void Login(IWebDriver driver, FspScraperOptions options)
        {
            // navigate to login page and login
            driver.Navigate().GoToUrl(options.LoginUrl);
            driver.FindElement(By.Id("username")).SendKeys(options.FspUser);
            driver.FindElement(By.Id("password")).SendKeys(options.FspPass + Keys.Enter);
            var url = driver.Url;
            Assert.AreEqual(options.AircraftUrl, url);
        }
        /// <summary>
        /// Scrapes times, creating a list of the label and a list of the values, and then calls the Parse method. Returns an ISet of an entity.
        /// </summary>
        public static ISet<Times> ScrapeTimes(IWebDriver driver, WebDriverWait wait)
        {
            // will hold our parsed time models
            var timesSet = new HashSet<Times>();

            // Finds out how many planes are present on the page
            var numPlanes = driver.WaitThenCountElement(wait, "//ul[@class='list-inline gallery ng-scope']/li");
            // Click on the aircraft options dropdown, and then aircraft times
            for (var i = 1; i <= numPlanes; i++)
            {
                driver.WaitThenClickElement(wait, $"//*[@id='main']/ul/li[{i}]/div[1]/a[2]/span");
                driver.WaitThenClickElement(wait, $"//*[@id='main']/ul/li[{i}]/div[1]/ul/li[4]/a");

                List<string> labels = driver.WaitThenGrabElementsText(wait, "//form[not (@aria-hidden='true')]/fieldset/div[not (@aria-hidden='true')and not(@class='ng-hide')]//label[(parent::div[not (@aria-hidden='true')])]");
                List<string> values = driver.WaitThenGrabElementsText(wait, "//form[not (@aria-hidden='true')]/fieldset/div[not (@aria-hidden='true')and not(@class='ng-hide')]//div[@class='input-holder' and (parent::div[not (@aria-hidden='true')])]/div");
                timesSet.Add(FspTimesParser.ParseTimes(labels, values));

                driver.WaitThenClickElement(wait, "//button[@class='close']");
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//button[@class='close']")));
            }
            return timesSet;
        }
    }
}
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection.Emit;
using System.Linq;
using System.Collections.Generic;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace CAM.Core.Services.TimesScraper
{
    /// <summary>
    /// Contains extension methods for use with Selenium.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Waits until the xpath exists, and then returns the count of matching elements.
        /// </summary>
        public static int WaitThenCountElement(this IWebDriver driver, WebDriverWait wait, string xpath)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(xpath)));

            return driver.FindElements(By.XPath(xpath)).Count;
        }
        /// <summary>
        /// Waits until the element is clickable, then clicks.
        /// </summary>
        public static void WaitThenClickElement(this IWebDriver driver, WebDriverWait wait, string xpath)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(xpath)));
            driver.FindElement(By.XPath(xpath)).Click();
        }
        /// <summary>
        /// Checks if the first element matching the xpath exists. 
        /// Once it exists, it runs WaitForAngularLoad and then returns the text of the element once Javascript loading is done.
        /// Will not allow empty strings.
        /// </summary>
        public static List<string> WaitThenGrabElementsText(this IWebDriver driver, WebDriverWait wait, string xpath)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(xpath)));
            WaitForAngularLoad(driver, wait);
            List<string> list = driver.FindElements(By.XPath(xpath)).Select(e => e.Text).ToList();
            return list;
        }
        /// <summary>
        /// Executes a script checking pendingRequests.length and waits until it returns zero. This isn't foolproof,
        /// but it should work in general for basic Javascript loading.
        /// </summary>
        public static void WaitForAngularLoad(IWebDriver driver, WebDriverWait wait)
        {
            var angReadyScript = "return angular.element(document.body).injector().get('$http').pendingRequests.length";
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            wait.Until(wd => js.ExecuteScript(angReadyScript).ToString() == "0");
        }
    }
}
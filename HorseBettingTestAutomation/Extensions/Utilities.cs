using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace HorseBettingTestAutomation.Extensions
{
    public enum Browser
    {
        Chrome,
        Firefox
    }

    public static class Utilities
    {
        /// <summary>
        /// Gets the webdriver object for the browser
        /// </summary>
        /// <param name="browser"></param>
        /// <returns></returns>
        public static IWebDriver GetWebdriver(Browser browser)
        {
            switch (browser)
            {
                case Browser.Chrome:
                    return new ChromeDriver();
                case Browser.Firefox:
                    return new FirefoxDriver();
                default:
                    return new ChromeDriver();
            }
        }

        /// <summary>
        /// Get value of browser from app settings
        /// </summary>
        /// <returns></returns>
        public static Browser GetBrowserFromAppSettings()
        {
            //Browser browser = new Browser();
            //browser = (Browser)new AppSettingsReader().GetValue("browser", browser.GetType());
            //return browser;

            string value = GetAppSettings("browser");
            Browser browser = (Browser)Enum.Parse(typeof(Browser), value);
            return browser;
        }

        /// <summary>
        /// get value from appsettings
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppSettings(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// Get full path relative to project executing directory
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        public static string GetFullPath(string relativePath)
        {
            string currentExecutableDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).ToString();
            string fullPath = Path.GetFullPath(Path.Combine(currentExecutableDir, relativePath));
            return fullPath;
        }

        /// <summary>
        /// Wait For Page load to be completed
        /// </summary>
        public static void WaitForPageReloadComplete()
        {
            IWebDriver driver = (IWebDriver)ScenarioContext.Current["Driver"];
            IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(e => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        /// <summary>
        /// Wait for element to be displayed and enabled
        /// </summary>
        /// <param name="element"></param>
        /// <param name="waitSeconds"></param>
        /// <returns></returns>
        public static bool WaitForElm(this IWebElement element, int waitSeconds)
        {
            IWait<IWebElement> wait = new DefaultWait<IWebElement>(element);
            wait.Timeout = TimeSpan.FromSeconds(waitSeconds);
            wait.PollingInterval = TimeSpan.FromMilliseconds(500);
            wait.Message = "Custom wait period of " + waitSeconds + " seconds expired for webelement " + element.Text;
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(ElementNotVisibleException));
            return wait.Until<bool>(e => e.Displayed && e.Enabled);
        }

        public static string GetUniqueTimeStamp()
        {
            string now = DateTime.Now.ToLongTimeString();
            return string.Concat("_", now.Replace(":", "_").Replace(" ", "_"));
        }

        /// <summary>
        /// Wait for element, maximum waitSeconds, and click the element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="waitSeconds"></param>
        /// <returns></returns>
        public static bool WaitAndClick(this IWebElement element, int waitSeconds)
        {
            if (WaitForElm(element, waitSeconds))
            {
                element.Click();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Wait for element maximum 30 seconds and click the element
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool WaitAndClick(this IWebElement element)
        {
            return WaitAndClick(element, 30);
        }

        /// <summary>
        /// Wait for element to be visible/enabled max 30 seconds and sendkeys
        /// </summary>
        /// <param name="element"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool WaitAndSendKeys(this IWebElement element, string text)
        {
            if (WaitForElm(element, 30))
            {
                element.SendKeys(text);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get the first visible element from the list of elements
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        public static IWebElement GetFirstVisibleElement(this IList<IWebElement> elements)
        {
            return elements.First(elm => elm.Displayed && elm.Enabled);
        }
    }
}

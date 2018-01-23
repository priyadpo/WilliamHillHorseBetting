using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using HorseBettingTestAutomation.Extensions;

namespace HorseBettingTestAutomation.Extensions
{
    [Binding]
    public sealed class Hooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        IWebDriver driver;
        [BeforeScenario]
        public void BeforeScenario()
        {
            Browser browser = Utilities.GetBrowserFromAppSettings();
            driver = Utilities.GetWebdriver(browser);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            ScenarioContext.Current.Add("Driver", driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}

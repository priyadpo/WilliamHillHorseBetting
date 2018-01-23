using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using TechTalk.SpecFlow;


namespace HorseBettingTestAutomation.Extensions
{
    public class PageObjectBase
    {
        protected IWebDriver driver;
        public PageObjectBase()
        {
            driver = (IWebDriver)ScenarioContext.Current["Driver"];
            var webElementWaitTime = TimeSpan.FromSeconds(30);
            var webElementPollingInterval = TimeSpan.FromMilliseconds(500);
            PageFactory.InitElements(this,
                new RetryingElementLocator(driver, webElementWaitTime, webElementPollingInterval));
        }
    }
}

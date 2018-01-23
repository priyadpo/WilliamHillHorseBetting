using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using HorseBettingTestAutomation.Extensions;
using TechTalk.SpecFlow;
using System.Collections;

namespace HorseBettingTestAutomation.Pageobjects
{
    public class Horse_Betting_future_stake : PageObjectBase
    {

        public string selectedHorseName = null; 

        [FindsBy(How = How.CssSelector, Using = "div.app-responsive-contentContainer")]
        private IWebElement elmRaceContainer;

        [FindsBy(How = How.CssSelector, Using = "div[id=FeatureRaces]>ul>li>a>b")]
        private IList<IWebElement> elmFeatureRaceList;

        [FindsBy(How = How.CssSelector, Using = "div[id=FeatureRaces]>ul>li.checked")]
        private IList<IWebElement> elmFeatureDefaultCheckedList;

        [FindsBy(How = How.CssSelector, Using = "section.block.current-race>header>h3>a.left")]
        private IWebElement elmRaceHeader;

        [FindsBy(How = How.CssSelector, Using = "section[data-feature-type=FeatureRaces]>div.block-inner.block-betting.block-racecard.block-multiples.block-feature-doubles>div.row>div>div")]
        private IList<IWebElement> elmRaceDetails;

        public Horse_Betting_future_stake():base()
        {
        }

        /// <summary>
        /// Navigate to the WilliamHill Feature Horse Races Website page
        /// </summary>
        /// <returns>Boolean Value</returns>
        public bool NavigateToHorseFutureStake()
        {
            try
            {
                var stakeUrl = Utilities.GetAppSettings("url");
                ScenarioContext.Current.Add("URL", stakeUrl);
                driver.Navigate().GoToUrl(stakeUrl);
                return elmRaceContainer.Displayed;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Select a Horse to Bet from Features List
        /// </summary>
        /// <param name="horseName"></param>
        /// <returns>Boolean Value</returns>
        public bool SelectHorse(string horseName)
        {
            try
            {
                var el_count = elmFeatureDefaultCheckedList.Count();
                if (el_count!= 0)
                {
                   elmFeatureDefaultCheckedList.First(e => e.FindElement(By.TagName("b")).Text.ToLower()!= horseName.ToLower()).WaitAndClick();
                }

                if (elmFeatureRaceList.Count != 0)
                {
                    elmFeatureRaceList.First(e => e.Text.ToLower() == horseName.ToLower()).WaitAndClick();
                    Utilities.WaitForPageReloadComplete();
                    selectedHorseName = horseName.ToLower();
                    return true;
                }
                else { return false; }
              
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        /// <summary>
        /// Select a Race card for the selected Horse race
        /// </summary>
        /// <param name="runner"></param>
        /// <returns></returns>
        public bool SelectRaceCard(string runner)
        {
            try
            {

                if (elmRaceHeader.Text.ToLower() == selectedHorseName)
                {
                    var el_count = elmRaceDetails.Count();
                     if (el_count != 1 || el_count != 0)
                      {
                        for (int index = 1; index < el_count; index++)
                        {
                            if(elmRaceDetails[index].FindElement(By.CssSelector("div:nth-child(1)")).Text.ToLower() == runner.ToLower())
                            {
                                elmRaceDetails[index].FindElement(By.CssSelector("div:nth-child(2)>ul>li>a")).WaitAndClick();
                                return true;
                            }                           
                         
                        }                           
                    }
                }
                return false;              
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}

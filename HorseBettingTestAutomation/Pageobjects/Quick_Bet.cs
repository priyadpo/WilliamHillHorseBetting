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
    public class Quick_Bet : PageObjectBase
    {
        [FindsBy(How = How.CssSelector, Using = "body>div:nth-child(36)> div")]
        private IWebElement elmQuickBetContainer;

        [FindsBy(How = How.CssSelector, Using = "input.CurrencyBox_currencyBox_b0O.CurrencyBox_withoutSymbol_87M.QuickBetOptions_textInput_3JR")]
        private IWebElement elmCurrency;

        [FindsBy(How = How.CssSelector, Using = "button.Button_button_1Hw.Button_color_28N.Button_transparent_31s")]
        private IWebElement btnAddtoBetSlip;

     /*   [FindsBy(How = How.CssSelector, Using = "button[class*=Button_button_1Hw.SideBar_button_Hgk.SideBar_buttonSelected_3Rx]")]
        private IWebElement elmQuickBetContainer;

        [FindsBy(How = How.CssSelector, Using = "input[class*=CurrencyBox_currencyBox_b0O.CurrencyBox_withoutSymbol_87M.AddStakeToAllTickets_stakeInput_3Em]")]
        private IWebElement elmStakeAmt;

        [FindsBy(How = How.CssSelector, Using = "button[class*=Button_button_1Hw.Button_color_28N.Button_green_3Ux.Betslip_placeBets_3xf]")]
        private IWebElement btnPlaceBet;*/

        public Quick_Bet():base()
        {
        }

        /// <summary>
        /// Enter the stake amount and add to the Betslip
        /// </summary>
        /// <param name="stakeAmt"></param>
        /// <returns></returns>
        public bool AddQuickBet(string stakeAmt)
        {
            try
            {
                if(elmQuickBetContainer.Displayed && elmQuickBetContainer.Enabled)
                {
                    elmCurrency.WaitAndSendKeys(stakeAmt);
                    btnAddtoBetSlip.WaitAndClick();
                    return true;
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

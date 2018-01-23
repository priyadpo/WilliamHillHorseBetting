using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using OpenQA.Selenium;
using HorseBettingTestAutomation.Extensions;
using HorseBettingTestAutomation.Pageobjects;
using System;



namespace HorseBettingTestAutomation.Features
{
    [Binding]
    public sealed class HorseBettingSteps
    {
        IWebDriver driver;
        Horse_Betting_future_stake future_stake;
        Quick_Bet quick_bet;

        [Given(@"I am in WilliamHill future horse betting site page")]
        public void GivenIAmInWilliamHillFutureHorseBettingSitePage()
        {
            future_stake = new Horse_Betting_future_stake();
            Assert.IsTrue(future_stake.NavigateToHorseFutureStake(), ScenarioContext.Current.StepContext.StepInfo.Text, "The website was loaded unsuccessfully");
        }

        [Given(@"I select the horse with name as ""(.*)"" from feature Race")]
        public void GivenISelectTheHorseWithNameAsFromFeatureRace(string horseName)
        {
            Assert.IsTrue(future_stake.SelectHorse(horseName), ScenarioContext.Current.StepContext.StepInfo.Text, "Select Horse action failed");
        }

        [When(@"I select my Racecard as Runner:""(.*)""")]
        public void WhenISelectMyRacecardAsRunnerType(string runner)
        {
            Assert.IsTrue(future_stake.SelectRaceCard(runner), ScenarioContext.Current.StepContext.StepInfo.Text, "Cannot select a Racecard");
        }

        [Then(@"I could add a stake as ""(.*)"" to the Bet Slip and confirm:")]
        public void ThenICouldAddAStakeAsToTheBetSlipAndConfirm(String stakeAmt)
        {
            quick_bet = new Quick_Bet();
            Assert.IsTrue(quick_bet.AddQuickBet(stakeAmt), ScenarioContext.Current.StepContext.StepInfo.Text, "Couldn't add a Stake for the selected Bet");
        }

    }
}

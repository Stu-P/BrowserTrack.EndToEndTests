using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using SpecflowAcceptanceTests.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpecflowUI.Framework;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;
using SpecResults;

namespace SpecflowAcceptanceTests.StepDefinitions
{


    [Binding]
    public class CommonSteps : FeatureBase
    {



        [BeforeFeature]
        public static void BeforeFeature()
        {
            FeatureContext.Current["TestLibraryAssembly"] = System.Reflection.Assembly.GetExecutingAssembly();
        }




        [When(@"I navigate to the dashboard page")]
        [Given(@"I am on the dashboard page")]
        public void NavigateToTheDashboardPage()
        {

            //  CurrentDriver.ResizeBrowserWindow(480, 750);

            string BaseURL = Environment.GetEnvironmentVariable("SystemUnderTest");
            if (string.IsNullOrEmpty(BaseURL))
            {
                throw new ArgumentNullException("SystemUnderTest URL not defined under environment variables");
            }

            NextPage = NewWindow.NavigateToDashboard(CurrentDriver, PageContentManager, BaseURL);



        }

        [Given(@"I have logged in and navigated to dashboard")]
        public void GivenIHaveLoggedInAndNavigatedToDashboard()
        {
            NavigateToTheDashboardPage();

            NextPage = CurrentPage.As<Dashboard>().ClickLogin();

            NextPage = CurrentPage.As<Login>().LoginWithValidCredentials("stutest", "Password12");
        }




        [When(@"I click the overdue alert icon")]
        public void WhenIClickTheOverdueAlertIcon()
        {
            CurrentPage.As<Dashboard>().ClickWarningIcon();
        }

        [When(@"I click on the chrome browser tile")]
        public void WhenIClickOnTheChromeBrowserTile()
        {
            CurrentPage.As<Dashboard>().ClickChromeTile();
        }



        [Then(@"a message ""(.*)"" is displayed")]
        public void ThenAWarningIsDisplayed(string warning)
        {
            CurrentPage.As<Dashboard>().AssertWarningMessageDisplayed(warning);
        }

        [Then(@"the browser switch is disabled")]
        public void ThenTheBrowserSwitchIsDisabled()
        {
            CurrentPage.As<Dashboard>().AssertSwitchDisabled();
        }

        [Then(@"the browser switch is enabled")]
        public void ThenThenTheBrowserSwitchIsEnabled()
        {
            CurrentPage.As<Dashboard>().AssertSwitchEnabled();
        }




        [Then(@"a grid of browsers is displayed containing")]
        public void ThenAGridOfBrowsersIsDisplayedContaining(Table table)
        {
            var browsers = CurrentPage.As<Dashboard>().GetBrowserTiles();
            table.CompareToSet<BrowserTile>(browsers);
        }


        [When(@"I click Version History from the menu")]
        public void WhenIClickVersionHistoryFromTheMenu()
        {
            NextPage = CurrentPage.As<Dashboard>().ClickVersionHistory();
        }


        [Then(@"a list of version changes is displayed")]
        public void ThenAListOfVersionChangesIsDisplayed(Table table)
        {
            var expectedItems = table.Rows.Select(o => o["VersionChange"]).ToList();

            var actualItems = CurrentPage.As<VersionHistory>().GetHistoryItems();

            CollectionAssert.AreEqual(expectedItems, actualItems, StringComparer.InvariantCultureIgnoreCase);

        }





        [Given(@"so and so")]
        public void GivenSoAndSo()
        {

        }

        [When(@"I do something")]
        public void WhenIDoSomething()
        {

        }

        [When(@"I do something else")]
        public void WhenIDoSomethingElse()
        {


        }

        [Then(@"I get this result")]
        public void ThenIGetThisResult()
        {

        }



    }


}
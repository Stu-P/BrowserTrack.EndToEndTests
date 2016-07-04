using System;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;
using SpecflowUI.Framework;
using SpecflowUI.Framework.Attributes;
using SpecflowUI.Framework.Selenium;



namespace SpecflowAcceptanceTests.PageObjects
{
    //[CheckPageTitle, CheckPageText(FindWith = With.ClassName, Using = "heroLink-text")]
    [CheckPageTitle]
    public class FrontPage : Common
    {



        //todo beter way of ensuring we land on the right country context
        public static string URL = "/investor/mobile/tomobile?cc=AU&lang=en";
        public static string URL_UAT = "/investor/mobile/mobile/tomobile?cc=AU&lang=en";


        [FindsBy(How = How.CssSelector, Using = "a[href='#Home/Tour']")]
        private IWebElement LearnMoreLink;

         [FindsBy(How = How.CssSelector, Using = "a[data-message='system-login']")]
        private IWebElement LoginCTALink;






    }
}

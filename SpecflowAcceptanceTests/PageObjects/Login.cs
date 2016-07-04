using SpecflowUI.Framework.Attributes;
using SpecflowUI.Framework.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpecflowUI.Framework;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;

namespace SpecflowAcceptanceTests.PageObjects
{
    [CheckPageText(FindWith = With.ClassName, Using = "card-header")]
    public class Login : Common
    {
        public static string URL = "/#/login";


        [FindsBy(How = How.Id, Using = "user")]
        private IWebElement UsernameField { get; set; }

        [FindsBy(How = How.Id, Using = "pass")]
        private IWebElement PasswordField { get; set; }

        [FindsBy(How = How.Id, Using = "submit")]
        private IWebElement LoginButton { get; set; }
        

        internal Dashboard LoginWithValidCredentials(string user, string pass)
        {
            UsernameField.SendKeys(user);
            PasswordField.SendKeys(pass);
            LoginButton.Click();

            return GetInstance<Dashboard>();
        }
    }
}

using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SpecflowUI.Framework;

namespace SpecflowAcceptanceTests.PageObjects
{
    public class Common : PageBase
    {


        #region Navigation Menu Elements

        [FindsBy(How = How.Id, Using = "login-btn")]
        private IWebElement LoginButton { get; set; }

        [FindsBy(How = How.Id, Using = "signup-btn")]
        private IWebElement SignupButton { get; set; }

        [FindsBy(How = How.Id, Using = "welcome-msg")]
        private IWebElement WelcomeText { get; set; }

        [FindsBy(How = How.Id, Using = "logout-btn")]
        private IWebElement LogoutButton { get; set; }



        [FindsBy(How = How.LinkText, Using = "Version History")]
        private IWebElement VersionHistoryMenuItem { get; set; }


        [FindsBy(How = How.LinkText, Using = "Contact")]
        private IWebElement ContactMenuItem { get; set; }
        #endregion

        public Login ClickLogin() {
            LoginButton.Click();
            return GetInstance<Login>();
        }

        public SignUp ClickSignup()
        {
            SignupButton.Click();
            return GetInstance<SignUp>();
        }

        public Dashboard ClickLogout()
        {
            LogoutButton.Click();
            return GetInstance<Dashboard>();
        }

        public VersionHistory ClickVersionHistory()
        {
            ContactMenuItem.Click();
            return GetInstance<VersionHistory>();
        }

        public Contact ClickContact()
        {
            LogoutButton.Click();
            return GetInstance<Contact>();
        }



        public override void WaitForDynamicPageLoading()
        {



            Driver.WaitForAjaxtoFinish(60, 1);
            System.Threading.Thread.Sleep(1000);

        }



        //public PageBase FullSiteLinkFromMenu(bool confirm = true)
        //{
        //    WaitUpTo(5000, OpenIcon.IsElementClickable, "Hamburger menu not clickable");

        //    OpenIcon.Click();
        //    WaitForNavbarSlide();
        //    WaitUpTo(5000, FullSiteLink.IsElementClickable, "Fullsite Icon not clickable");

        //    FullSiteLink.Click();
        //    System.Threading.Thread.Sleep(500);

        //    if (confirm)
        //    {
        //        Driver.FindElement(By.CssSelector("button.btn-primary[data-dismiss='modal']"), 5).Click();
        //        //  ModalConfirmBtn.Click();
        //        System.Threading.Thread.Sleep(1000);
        //        return GetInstance<DesktopPortfolioPage>(waitForDynamic: false);
        //    }
        //    else
        //    {
        //        ModalCancelBtn.Click();
        //        return this;
        //    }
        //}

        //public TnCPage ClickTnCLinkFromMenu()
        //{
        //    WaitUpTo(5000, OpenIcon.IsElementClickable, "Hamburger menu not clickable");

        //    OpenIcon.Click();
        //    WaitForNavbarSlide();
        //    WaitUpTo(5000, TnCLink.IsElementClickable, "SysReqLink not clickable");

        //    TnCLink.Click();
        //    WaitForNavbarSlide();
        //    return GetInstance<TnCPage>();
        //}




        //public AddressUpdateEntryPage ClickAddressUpdateFromMenu()
        //{
        //    OpenIcon.Click();
        //    WaitForNavbarSlide();
        //    AddressUpdateLink.Click();
        //    WaitForNavbarSlide();
        //    return GetInstance<AddressUpdateEntryPage>();
        //}






    }
}
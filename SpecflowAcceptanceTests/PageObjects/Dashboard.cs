using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SpecflowUI.Framework.Attributes;
using SpecflowUI.Framework.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecflowAcceptanceTests.PageObjects
{
    [CheckPageText(FindWith = With.ClassName, Using = "card-header")]
    public class Dashboard : Common
    {
        public static string URL = "/#/";


        [FindsBy(How = How.CssSelector, Using = "div.BrowserGridList")]
        private IWebElement GridList;

        [FindsBy(How = How.CssSelector, Using = "md-grid-tile.Chrome")]
        private IWebElement ChromeTile { get; set; }

        [FindsBy(How = How.Id, Using = "rp-update-switch")]
        private IWebElement VersionCheckSwitch{ get; set; }

        

        [FindsBy(How = How.Id, Using = "warning-button")]
        private IWebElement WarningIcon { get; set; }

        [FindsBy(How = How.Id, Using = "warning-panel")]
        private IWebElement WarningPanel { get; set; }

        public void ClickWarningIcon()
        {
            WarningIcon.Click();
        }

        internal void ClickChromeTile()
        {
            ChromeTile.Click();
        }


        public void AssertWarningMessageDisplayed(string expectedText)
        {
            System.Threading.Thread.Sleep(500);
            AssertElementText(WarningPanel, expectedText, "Warning Text");
        }


        public void AssertSwitchDisabled()
        {
            string status = VersionCheckSwitch.GetAttribute("disabled");
            Assert.AreEqual(status, "true", "Switch status");
            
        }
        public void AssertSwitchEnabled()
        {
            string status = VersionCheckSwitch.GetAttribute("disabled");
            Assert.IsNull(status, "Switch status");

        }


        public List<BrowserTile> GetBrowserTiles()
        {


            var browsers = new List<BrowserTile>();

            var elements = GridList.FindElements(By.TagName("md-grid-tile"));
            foreach (var el in elements)
            {
                browsers.Add(
                    new BrowserTile
                    {
                        BrowserName = el.FindElement(By.TagName("md-grid-tile-header")).Text,
                        BrowserVersion = el.FindElement(By.TagName("md-grid-tile-footer")).Text
                    }
                    );

            }
            return browsers;

        }


    }

    public class BrowserTile
    {
        public string BrowserName { get; set; }
        public string BrowserVersion { get; set; }

    }
}

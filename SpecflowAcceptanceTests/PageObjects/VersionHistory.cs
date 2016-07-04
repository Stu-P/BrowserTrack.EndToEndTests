using OpenQA.Selenium;
using SpecflowUI.Framework.Attributes;
using SpecflowUI.Framework.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecflowAcceptanceTests.PageObjects
{
    [CheckPageText(FindWith = With.ClassName, Using = "card-header")]
    public class VersionHistory : Common
    {
        public static string URL = "/#/versionhistory";



        public List<string> GetHistoryItems()
        {

            var items = Driver.FindElements(By.CssSelector("md-list md-list-item"));

            var st = new List<string>();

            foreach (var e in items) {
                st.Add(e.Text.Trim());
            }

            return st;
        }
    }
}

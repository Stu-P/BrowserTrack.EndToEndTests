using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpecflowUI.Framework;
using OpenQA.Selenium;
using System.Resources;

namespace SpecflowAcceptanceTests.PageObjects
{
    public abstract class NewWindow : Common
    {

        public static Dashboard NavigateToDashboard(IWebDriver driver, ResourceManager resourceManager, string _baseUrl)
        {
            driver.Navigate().GoToUrl(_baseUrl.TrimEnd(new char[] { '/' }) + Dashboard.URL);
            return GetInstance<Dashboard>(driver,resourceManager, _baseUrl, true);
        }

  

    }
}


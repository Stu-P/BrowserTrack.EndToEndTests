using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using System.Drawing.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpecflowUI.Framework.Util;
using System.Configuration;
using System.Resources;
using System.Reflection;
using SpecResults;

namespace SpecflowUI.Framework
{
    //TODO get rid of logic to keep browser alive between tests, too much overhead

    public class TestClassBase : ReportingStepDefinitions
    {
        // private static Logger logger = LogManager.GetCurrentClassLogger();

        private const bool KeepBrowserAlive = false;

        protected static RemoteWebDriver CurrentDriver { get; set; }
        protected static ResourceManager PageContentManager;

        public static void OpenBrowser()
        {
            Browser.IsOpen = true;

            var seleniumDriverPath = ConfigurationManager.AppSettings["SeleniumDriverPath"].ToString();

            if (string.IsNullOrEmpty(seleniumDriverPath))
            {
                throw new ArgumentNullException("SeleniumDriverPath not defined in app settings");
            }

            var timeout = Convert.ToDouble(ConfigurationManager.AppSettings["Timeout"].ToString());

            switch (Browser.UnderTest)
            {
                case "firefox":
                    CurrentDriver = new FirefoxDriver(new FirefoxBinary(), new FirefoxProfile(), TimeSpan.FromSeconds(timeout));
                    break;

                case "chrome":
                    CurrentDriver = new ChromeDriver(seleniumDriverPath, new ChromeOptions(), TimeSpan.FromSeconds(timeout));
                    break;

                default:
                    CurrentDriver = new InternetExplorerDriver(seleniumDriverPath);
                    break;
            }


            CurrentDriver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(timeout));

        }

        public static void CloseBrowser()
        {
            Browser.IsOpen = false;
            try
            {
                CurrentDriver.Quit();
            }
            catch { }
        }

        public static void Test_Setup(Assembly testLibraryAssembly)
        {

            // Set the culture 
            var myTestCulture = ConfigurationManager.AppSettings["Culture"].ToString();
            CultureManager.SetCulture(myTestCulture);

            Browser.UnderTest = ConfigurationManager.AppSettings["DefaultBrowser"];


            if (PageContentManager == null)
            {
                var resourcesLocation = ConfigurationManager.AppSettings["LanguageResources"].ToString();
                PageContentManager = new System.Resources.ResourceManager(resourcesLocation, testLibraryAssembly);
            }

            OpenBrowser();

        }

        protected static void Test_Teardown()
        {
            try
            {
                // take screenshot on test failure
                if ((ScenarioContext.Current.TestError != null) && CurrentDriver is ITakesScreenshot)
                {
                    string resultsDir = @"C:/Test_Results/";
                    System.IO.Directory.CreateDirectory(resultsDir);

                    string filename = resultsDir + ScenarioContext.Current.ScenarioInfo.Title + ".jpg";

                    ((ITakesScreenshot)CurrentDriver).GetScreenshot().SaveAsFile(filename, ImageFormat.Jpeg);

                    string source_filename = resultsDir + ScenarioContext.Current.ScenarioInfo.Title + ".txt";
                    var source = CurrentDriver.PageSource;

                    PageSourceUtility.SaveErrorSource(source_filename, source);

                }
            }
            catch
            {
                System.Console.WriteLine("Unable to save screenshot and/or pagesource for test outcome");
            }

            finally
            {

                CloseBrowser();
            }


        }

        public static void Class_Setup()
        {
            //// Set the culture 
            //var myTestCulture = ConfigurationManager.AppSettings["Culture"].ToString();
            //CultureManager.SetCulture(myTestCulture);

            //Browser.UnderTest = ConfigurationManager.AppSettings["DefaultBrowser"];
            //// OpenBrowser();
        }

        public static void Class_Teardown()
        {
            //Browser.IsOpen = false;
            //try
            //{
            //    CurrentDriver.Quit();
            //}
            //catch { }
        }
    }

    public static class Browser
    {
        public static string UnderTest { get; set; }
        public static bool IsOpen { get; set; }
    }
}

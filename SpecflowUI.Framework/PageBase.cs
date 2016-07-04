using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpecflowUI.Framework.Util;
using System.Reflection;
using SpecflowUI.Framework.Attributes;
using System.Resources;
using SpecflowUI.Framework.Selenium;
using System.Configuration;

namespace SpecflowUI.Framework
{
    public abstract class PageBase : PageExtensions
    {
      //  private static Logger logger = LogManager.GetCurrentClassLogger();

        public string BaseURL { get; set; }

        public abstract void WaitForDynamicPageLoading();


        protected TPage GetInstance<TPage>(IWebDriver driver = null, ResourceManager resourceManager = null, bool waitForDynamic = true) where TPage : PageBase, new()
        {
            return GetInstance<TPage>(driver ?? Driver, resourceManager ?? MyResourceManager, BaseURL, waitForDynamic);
        }

        protected static TPage GetInstance<TPage>(IWebDriver driver, ResourceManager resourceManager, string baseUrl, bool waitForDynamic) where TPage : PageBase, new()
        {

            var timeout = Convert.ToDouble(ConfigurationManager.AppSettings["Timeout"].ToString());


            new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(timeout))
                                            .Until<IWebElement>((d) =>
                                            {
                                                return d.FindElement(ByChained.TagName("body"));

                                            });

            TPage pageInstance = new TPage()
            {
                Driver = driver,
                MyResourceManager = resourceManager,
                BaseURL = baseUrl
            };
            PageFactory.InitElements(driver, pageInstance);

            var pageName = pageInstance.GetType().Name;


            //Wait for dynamic content on the page to load
            if (waitForDynamic)
            {
                pageInstance.WaitForDynamicPageLoading();
            }


            // Check Page title if attribute declared for page object
            var checkPageTitle = GetAttribute<CheckPageTitleAttribute>(typeof(TPage));
            if (checkPageTitle == null)
            {
                //Log this case ?
            }
            else
            {
                var expectedTitle = resourceManager.GetPageTitleFromResource(pageName).ToLower();
                StringAssert.Contains(driver.Title.ToLower().Trim(), expectedTitle, "Unexpected page, incorrect title '{0}'", pageName);
            }


            // Check for unique page text on page if attribute declared
            var checkPageText = GetAttribute<CheckPageTextAttribute>(typeof(TPage));

            if (checkPageText != null)
            {
                var locator = ByFactory.Using(checkPageText);

                var expectedText = resourceManager.GetUniqueStringFromResource(pageName);


                var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.IgnoreExceptionTypes(typeof(ArgumentNullException));
                try
                {
                    wait.Until<bool>((d) =>
                    {
                        var elements = d.FindElements(locator);

                        return elements.Any(x => string.Compare(expectedText, x.Text.Trim(), true) == 0);
                       // var actualText = d.FindElement(locator).Text.Trim();
                       // return (string.Compare(expectedText, actualText, true) == 0);
                    });
                }
                catch (WebDriverTimeoutException ex) {
                    throw new WebDriverTimeoutException(string.Format("Checking for page: {0}, expected unique text {1} not found", pageName, expectedText) + ex.InnerException);
                } 
                
                //var actualText = wait.Until<IWebElement>((d) =>
                //{
                //    return d.FindElement(locator);
                //}).Text;

                //StringAssert.Contains(actualText, expectedText, "Unexpected page, incorrect page text '{0}'", pageName);

            }



            return pageInstance;
        }






        /// <summary>
        /// Asserts that the current page is of the given type
        /// </summary>
        public void Is<TPage>() where TPage : PageBase, new()
        {
            if (!(this is TPage))
            {
                throw new AssertFailedException(String.Format("Page Type Mismatch: Current page is not  '{0}'", typeof(TPage).Name));
            }
        }


        /// <summary>
        /// Inline cast to the given page type
        /// </summary>
        public TPage As<TPage>() where TPage : PageBase, new()
        {
            return (TPage)this;
        }



        public static TAttribute GetAttribute<TAttribute>(Type t) where TAttribute : System.Attribute, new()
        {
            // Get instance of the attribute.
            var MyAttribute =
                (TAttribute)Attribute.GetCustomAttribute(t, typeof(TAttribute));


            if (MyAttribute == null)
            {
            //    Console.WriteLine("Error - Attribute missing.");

            }
            return MyAttribute;
        }






    }
}
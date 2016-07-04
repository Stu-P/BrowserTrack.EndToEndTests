using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace SpecflowUI.Framework
{
    public static class WebDriverExtensions
    {

        public const int WaitSeconds = 30;

        public static bool IsElementPresent(this IWebElement element) {
            try {
                bool b = element.Displayed;
                return b;
            }
            catch  {
                return false;
            }
        }


        public static bool IsElementPresent(this ISearchContext context, By searchBy)
        {
            try
            {
                bool b = context.FindElement(searchBy).Displayed;
                return b;
            }
            catch
            {
                return false;
            }
        }

        public static string GetElementTextIfExists(this ISearchContext context, By searchBy)
        {
            return IsElementPresent(context, searchBy) ? context.FindElement(searchBy).Text.Trim() : "";

        }







        public static bool IsAttributePresent(this IWebElement element, string attribute, string value)
        {
            try
            {
                bool b = element.GetAttribute(attribute).Contains(value);
                return b;
            }
            catch 
            {
                return false;
            }
        }


        public static IWebElement FindElement(this ISearchContext context, By by, uint timeout, bool displayed = false)
        {
            var wait = new DefaultWait<ISearchContext>(context);
            wait.Timeout = TimeSpan.FromSeconds(timeout);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            return wait.Until(ctx =>
            {
                var elem = ctx.FindElement(by);
                if (displayed && !elem.Displayed)
                    return null;

                return elem;
            });
        }


        public static bool IsElementVisible(this IWebElement element)
        {
            try
            {
                bool b = element.Displayed && element.Enabled;

                return b;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsElementClickable(this IWebElement element)
        {
            try
            {
                bool b = element.Displayed && element.Enabled;

                return b;
            }
            catch
            {
                return false;
            }
        }



        public static void ClickElementByPartialLinkText(this IWebElement element, string linkText)
        {
            try
            {
                element.FindElement(By.PartialLinkText(linkText)).Click();
            }
            catch (NoSuchElementException)
            {
                Assert.Fail("Element was not found with linktext " + linkText);
            }
            catch (TimeoutException)
            {
                Assert.Fail("Element was not found with linktext " + linkText);
            }
        }



        public static ReadOnlyCollection<IWebElement> GetTableRows(this IWebElement element) {
            try
            {
                return element.FindElements(By.CssSelector("tbody tr"));
            }
            catch
            {
                throw new NoSuchElementException("No table rows found ");
            }

        }

        public static ReadOnlyCollection<IWebElement> GetCellsForTableRow(this IWebElement element)
        {
            try
            {
                return element.FindElements(By.CssSelector("td"));
            }
            catch
            {
                throw new NoSuchElementException("No cells found for table row");
            }
        }

        public static void ClickTableCellInput(this IWebElement element)
        {
            try
            {
                 element.FindElement(By.TagName("input")).Click();
            }
            catch
            {
                throw new NoSuchElementException("input not found");
            }
        }



        public static List<string> GetTableHeadersAsStringList(this IWebElement element) {

            var headerCells = element.FindElements(By.CssSelector("thead th"));
             

            var result = new List<string>();
            foreach (var cell in headerCells)
            {
                result.Add(cell.Text.Trim());
            }
            return result;
        }

        public static List<List<string>> GetAllTableRowsAsStringList(this IWebElement element)
        {
            var rows = element.FindElements(By.CssSelector("tbody tr"));

            var allRowsList = new List<List<string>>();

            foreach (var row in rows) {

                var rowList = new List<string>();

                var cells = row.FindElements(By.TagName("td"));

                foreach (var cell in cells) {
                    rowList.Add(cell.Text.Trim());
                }
                allRowsList.Add(rowList);

            }
            return allRowsList;
            
        }


        public static IWebElement FindTableRowByText(this ISearchContext context, string searchText) {

            try
            {
                return context.FindElements(By.TagName("tr")).First(x => x.Text.Contains(searchText));
            }
            catch {
                throw new NoSuchElementException("No table row found containing text " + searchText);
            }
        }

        public static IWebElement FindListItemByText(this ISearchContext context, string searchText)
        {

            try
            {
                return context.FindElements(By.TagName("li")).First(x => x.Text.Contains(searchText));
                }
            catch
            {
                throw new NoSuchElementException("No list item found containing text " + searchText);
            }
        }

        public static IWebElement FindByTagAndPartialText(this ISearchContext context, string tagName, string searchText)
        {

            try
            {
                return context.FindElements(By.TagName(tagName)).First(x => x.Text.Contains(searchText));
            }
            catch
            {
                throw new NoSuchElementException("No item found containing text " + searchText);
            }
        }



        //public static void Highlight(this IWebElement element)
        //{
        //    const int wait = 150;
        //    string orig = element.GetAttribute("style");
        //    SetAttribute(element, "style", "color: yellow; border: 10px solid yellow; background-color: black;");
        //    Thread.Sleep(wait);
        //    SetAttribute(element, "style", "color: black; border: 10px solid black; background-color: yellow;");
        //    Thread.Sleep(wait);
        //    SetAttribute(element, "style", "color: yellow; border: 10px solid yellow; background-color: black;");
        //    Thread.Sleep(wait);
        //    SetAttribute(element, "style", "color: black; border: 10px solid black; background-color: yellow;");
        //    Thread.Sleep(wait);
        //    SetAttribute(element, "style", orig);
        //}



        ///* doesnt work */
        //public static void SetAttribute(this IWebElement element, string attributeName, string value)
        //{

        //    //IWebElement webElement =(IWebElement) element ;
        //    IWrapsDriver wrappedElement = element as IWrapsDriver;

        //    if (wrappedElement == null)
        //    {
        //        FieldInfo fieldInfo = element.GetType().GetField("underlyingElement", BindingFlags.NonPublic | BindingFlags.Instance);
        //        if (fieldInfo != null)
        //        {
        //            wrappedElement = fieldInfo.GetValue(element) as IWrapsDriver;
        //            if (wrappedElement == null)
        //            {
        //                throw new ArgumentException("element", "Element must wrap a web driver");
        //            }
        //        }
        //    }




        //    IWebDriver driver = wrappedElement.WrappedDriver;
        //    IJavaScriptExecutor javascript = driver as IJavaScriptExecutor;
        //    if (javascript == null)
        //        throw new ArgumentException("element", "Element must wrap a web driver that supports javascript execution");

        //    javascript.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2])", element, attributeName, value);
        //}









        public static void ClickElementById(this IWebDriver driver, string elementId)
        {
            try
            {
                driver.WaitForElementById(elementId).Click();
            }
            catch (NoSuchElementException)
            {
                Assert.Fail("Element was not found with id " + elementId);
            }
            catch (TimeoutException)
            {
                Assert.Fail("Element was not found with id " + elementId);
            }
        }

        public static IWebElement WaitForElementById(this IWebDriver driver, string elementId)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, WaitSeconds));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(NoSuchElementException), typeof(NotFoundException));
            return wait.Until(d =>
            {
                try
                {
                    var foundElement = d.FindElement(By.Id(elementId));
                    return foundElement;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            });
        }

        //public static bool WaitForAttributeValue(this IWebElement element, uint timeout, string attribute, string value)
        //{
        //    var wait = new DefaultWait<ISearchContext>(element);
        //    wait.Timeout = TimeSpan.FromSeconds(timeout);
        //    wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
        //    return wait.Until(ctx =>
        //    {
        //        if (element.GetAttribute(attribute).Contains(value))
        //            return true;

        //        return false;
        //    });
        //}

        //public static bool WaitForAttributeValueRemoval(this IWebElement element, uint timeout, string attribute, string value)
        //{
        //    var wait = new DefaultWait<ISearchContext>(element);
        //    wait.Timeout = TimeSpan.FromSeconds(timeout);
        //    wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
        //    return wait.Until(ctx =>
        //    {
        //        if (!element.GetAttribute(attribute).Contains(value))
        //            return true;

        //        return false;
        //    });
        //}


        public static void TakeScreenshot(this IWebDriver driver, string outputFile)
        {
            var outputFolder = Path.GetDirectoryName(outputFile);
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            var takesScreenshot = driver as ITakesScreenshot;

            if (takesScreenshot != null)
            {
                var screenshot = takesScreenshot.GetScreenshot();

                var screenshotFilePath = Path.Combine(outputFile);

                screenshot.SaveAsFile(screenshotFilePath, ImageFormat.Png);
            }
        }

        public static void ClickCertificateError(this IWebDriver driver)
        {
            if (driver.Title.Contains("Certificate Error"))
                driver.Navigate().GoToUrl("javascript:document.getElementById('overridelink').click()");
        }

        public static void WaitForAjaxtoFinish(this IWebDriver driver, int secondsToWait, int pulseInterval)
        {
            new WebDriverWait(new SystemClock(), driver, TimeSpan.FromSeconds(secondsToWait), TimeSpan.FromMilliseconds(pulseInterval))
                .Until(browser => (long)browser.Scripts().ExecuteScript("return jQuery.active") == 0);
        }

        public static IJavaScriptExecutor Scripts(this IWebDriver browser)
        {
            return browser as IJavaScriptExecutor;
        }



        public static void ResizeBrowserWindow(this IWebDriver driver, int width, int height)
        {
            driver.Manage().Window.Size = new Size(width, height);

        }

        public static void MaximizeBrowserWindow(this IWebDriver driver)
        {
            driver.Manage().Window.Maximize();

        }




    }
}

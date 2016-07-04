using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;
using System.Resources;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;
using System.Collections.Specialized;
using SpecflowUI.Framework.Util;

namespace SpecflowUI.Framework
{
    public abstract class PageExtensions
    {
        public IWebDriver Driver { get; set; }
        public ResourceManager MyResourceManager { get; set; }

        /// <summary>
        /// Clears any existing text in an text input field and types in new value
        /// </summary>
        /// <param name="element">Element to type into</param>
        /// <param name="value">Value to enter</param>
        /// <returns></returns>
        protected void ClearAndType(IWebElement element, string value)
        {
            WaitUpTo(5000, element.IsElementVisible, element.TagName);
            element.Clear();
            element.SendKeys(value);
            Thread.Sleep(100);

        }

        protected void Pause(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }

        /// <summary>
        /// Click on element, e.g. button, link, radio
        /// </summary>
        /// <param name="element">Element to click</param>
        /// <param name="elementName">description of element (optional)</param>
        /// <returns></returns>
        protected void Click(IWebElement element, string elementName = "")
        {

            WaitUpTo(5000, element.IsElementClickable, String.Format("Element not clickable: {0}", elementName));

            try
            {
                element.Click();
            }
            catch (System.InvalidOperationException ex)
            {
                System.Console.WriteLine("Click failed (due to animation?) retrying once" + ex.InnerException);

                System.Threading.Thread.Sleep(500);
                element.Click();

            }

        }



        protected void MatchTable(IWebElement actualTable, Table expectedTable) {

            var actualHeaders = actualTable.GetTableHeadersAsStringList();
            var actualRows = actualTable.GetAllTableRowsAsStringList();

            /* Compare headers */
            CollectionAssert.AreEqual( expectedTable.Header.ToList(), actualHeaders, "Actual table headers do not match specflow table ");

           
            /* Compare number of rows */
            Assert.AreEqual( expectedTable.RowCount, actualRows.Count, String.Format("Number of table rows do not match, expected {1} rows but actually found {0}", expectedTable.RowCount, actualRows.Count));
            /* Compare content all rows */

            for (int i = 0; i < actualRows.Count; i++) {

                var expectedRow = expectedTable.Rows[i].Values.ToList();
                CollectionAssert.AreEqual(expectedRow, actualRows[i], String.Format("Table row number {0} does not match specflow table", i+1));

            }
        }


        protected void MatchTableRow(IWebElement actualTable, Table expectedRow) {

            var actualHeaders = actualTable.GetTableHeadersAsStringList();
            var actualRows = actualTable.GetAllTableRowsAsStringList();

            /* Compare headers */
            CollectionAssert.AreEqual(expectedRow.Header.ToList(), actualHeaders, "Actual table headers do not match specflow table ");

            /* flatten table and find row */
            var expected = expectedRow.Rows.First().Values.ToList();
            var actual = actualRows.SelectMany(list => list).ToList();

            CollectionAssert.IsSubsetOf( expected, actual, "No matching row can be found in table");
        }


        protected void WaitUpTo(int milliseconds, Func<bool> Condition, string description)
        {
            int timeElapsed = 0;
            while (!Condition() && timeElapsed < milliseconds)
            {
                Thread.Sleep(100);
                timeElapsed += 100;
            }

            if (timeElapsed >= milliseconds || !Condition())
            {
                throw new AssertFailedException("Timed out while waiting for: " + description);
            }
        }


        //protected void WaitUntilElementNotVisible(By locator)
        //{
        //    var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));

        //    wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
        //}






        //protected static void AssertIsEqual(string expectedValue, string actualValue, string elementDescription)
        //{
        //    if (expectedValue != actualValue)
        //    {
        //        throw new AssertFailedException(String.Format("AssertIsEqual Failed: '{0}' didn't match expectations. Expected [{1}], Actual [{2}]", elementDescription, expectedValue, actualValue));
        //    }
        //}

        protected static void AssertElementPresent(IWebElement element, string elementDescription)
        {
            if (!element.IsElementPresent())
                throw new AssertFailedException(String.Format("AssertElementPresent Failed: Could not find '{0}'", elementDescription));
        }

        protected static void AssertElementNotPresent(IWebElement element, string elementDescription)
        {
            if (element.IsElementPresent())
                throw new AssertFailedException(String.Format("AssertElementNotPresent Failed: Found element which should not be visible'{0}'", elementDescription));
        }

        protected static void AssertElementPresent(ISearchContext context, By searchBy, string elementDescription)
        {
            if (!context.IsElementPresent( searchBy))
                throw new AssertFailedException(String.Format("AssertElementPresent Failed: Could not find '{0}'", elementDescription));
        }

        protected static void AssertElementNotPresent(ISearchContext context, By searchBy, string elementDescription)
        {
            if (context.IsElementPresent( searchBy))
                throw new AssertFailedException(String.Format("AssertElementNotPresent Failed: found '{0}'", elementDescription));
        }


        protected static void AssertElementsPresent(IWebElement[] elements, string elementDescription)
        {
            if (elements.Length == 0)
                throw new AssertFailedException(String.Format("AssertElementsPresent Failed: Could not find '{0}'", elementDescription));
        }

        protected static void AssertElementText(IWebElement element, string expectedValue, string elementDescription)
        {
            AssertElementPresent(element, elementDescription);
            string actualtext = element.Text;
            if (actualtext != expectedValue)
            {
                throw new AssertFailedException(String.Format("AssertElementText Failed: Value for '{0}' did not match expectations. Expected: [{1}], Actual: [{2}]", elementDescription, expectedValue, actualtext));
            }
        }

        protected static void AssertElementTextContains(IWebElement element, string expectedValue, string elementDescription)
        {
            AssertElementPresent(element, elementDescription);
            string actualtext = element.Text;
            if (!actualtext.Contains(expectedValue))
            {
                throw new AssertFailedException(String.Format("AssertElementTextContains Failed: Value for '{0}' did not match expectations. Expected: [{1}], Actual: [{2}]", elementDescription, expectedValue, actualtext));
            }
        }

        protected static void AssertElementTextNotContains(IWebElement element, string expectedValue, string elementDescription)
        {
            AssertElementPresent(element, elementDescription);
            string actualtext = element.Text;
            if (actualtext.Contains(expectedValue))
            {
                throw new AssertFailedException(String.Format("AssertElementTextNotContains Failed: Value for '{0}' did not match expectations. Expected: [{1}], Actual: [{2}]", elementDescription, expectedValue, actualtext));
            }
        }

        protected static void AssertElementTextMatchesRegex(IWebElement element, string regex, string elementDescription)
        {

            AssertElementPresent(element, elementDescription);

            string actualtext = element.Text;
            if (!Regex.IsMatch(actualtext, regex)) 
            {
                throw new AssertFailedException(String.Format("AssertElementTextMatchesRegex Failed: Value for '{0}' did not match regex. ExpectedRegex: [{1}], ActualText: [{2}]", elementDescription, regex, actualtext));
            }
        }

        protected static void AssertElementHasAttribute(IWebElement element, string attribute, string value, string elementDescription)
        {
            AssertElementPresent(element, elementDescription);

            var isTrue = element.GetAttribute(attribute).Contains(value);

            if (!isTrue)
            {
                throw new AssertFailedException(String.Format("AssertElementHasAttribute Failed for element {0}, expected attribute {1} to contain value {2}", elementDescription, attribute, value));
            }
        }


        protected bool WaitForAttributeValue(By locator, uint timeout, string attribute, string value)
        {
            var wait = new DefaultWait<ISearchContext>(Driver);
            wait.Timeout = TimeSpan.FromSeconds(timeout);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
            return wait.Until(ctx =>
            {
                var attr = Driver.FindElement(locator).GetAttribute(attribute);
                if (attr.Contains(value))
                    return true;

                return false;
            });
        }




        protected bool WaitForAttributeValueRemoval(By locator, uint timeout, string attribute, string value)
        {
            var wait = new DefaultWait<ISearchContext>(Driver);
            wait.Timeout = TimeSpan.FromSeconds(timeout);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
            return wait.Until(ctx =>
            {
                var elem = Driver.FindElement(locator);
                if (elem.GetAttribute(attribute).Contains(value) == false)
                    return true;

                return false;
            });
        }
    }

}

using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SpecflowUI.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecflowUI.Framework.Selenium
{
    public enum With
    {
        Id, Name, TagName, ClassName, CssSelector, LinkText, PartialLinkText, XPath
    }


    public static class ByFactory
    {

        public static By Using(CheckPageTextAttribute attribute)
        {
            return Using(attribute.FindWith, attribute.Using);

        }

        public static By Using(With _with, string _using)
        {

            switch (_with)
            {
                case With.Id:
                    return By.Id(_using);
                case With.Name:
                    return By.Name(_using);
                case With.TagName:
                    return By.TagName(_using);
                case With.ClassName:
                    return By.ClassName(_using);
                case With.CssSelector:
                    return By.CssSelector(_using);
                case With.LinkText:
                    return By.LinkText(_using);
                case With.PartialLinkText:
                    return By.PartialLinkText(_using);
                case With.XPath:
                    return By.XPath(_using);
            }
            throw new ArgumentException("Cant Understand Find element attributes");
        }
    }
}

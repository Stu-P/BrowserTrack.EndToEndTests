using SpecflowUI.Framework.Attributes;
using SpecflowUI.Framework.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecflowAcceptanceTests.PageObjects
{
    [CheckPageText(FindWith = With.ClassName, Using = "card-header")]
    public class Contact : Common
    {
        public static string URL = "/#/contact";

    }
}

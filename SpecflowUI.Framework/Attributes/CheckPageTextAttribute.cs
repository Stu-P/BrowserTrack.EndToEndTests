using SpecflowUI.Framework.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecflowUI.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class CheckPageTextAttribute : System.Attribute
    {
        public CheckPageTextAttribute()
        {
            CheckTitle = true;

        }

        public bool CheckTitle{ get; set; }
        public With FindWith { get; set; }
        public string Using { get; set; }
        
    }
}



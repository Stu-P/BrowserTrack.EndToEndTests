using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecflowUI.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class CheckPageTitleAttribute : System.Attribute
    {
        public CheckPageTitleAttribute() {
            CheckTitle = true;
        }
        public bool CheckTitle { get; set; }

    }
}



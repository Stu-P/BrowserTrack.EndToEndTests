﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecflowUI.Framework.Util
{
    public static class StringExtensions
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }
    }
}

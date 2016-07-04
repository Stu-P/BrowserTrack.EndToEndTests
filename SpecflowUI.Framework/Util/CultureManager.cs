using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SpecflowUI.Framework.Util
{
    public static class CultureManager
    {
        public static void SetCulture(string _culture) {
            
            //is culture supported?
            var newCulture = new CultureInfo(_culture);
            SetCulture(newCulture);
  
        }


        public static void SetCulture(CultureInfo _newCulture)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = _newCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = _newCulture;
        
        }
    }
}

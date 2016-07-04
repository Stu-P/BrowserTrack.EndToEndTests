using System;
using System.IO;

namespace SpecflowUI.Framework
{
    class PageSourceUtility
    {

        private static readonly object Sync = new object();

        public static void SaveErrorSource(string filename, string pageSource)
        {
            
            SaveSource(pageSource, filename);
        }

        private static void SaveSource(string pageSource, string filename)
        {
            lock (Sync)
            {
                StreamWriter file = null;
                try
                {
                    file = new StreamWriter(filename);
                    file.Write(pageSource);
                    file.Flush();
                }
                finally
                {
                    if (file != null)
                    {
                        file.Close();
                    }
                }
            }
        }
    }
}

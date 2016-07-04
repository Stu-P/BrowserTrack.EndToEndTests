using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;


namespace SpecflowUI.Framework.Util
{
    public static class TfsUtils
    {
        public static string GetTestConfiguration(TestContext context)
        {
            if (context.Properties.Contains("__Tfs_TestConfigurationName__"))
            {
                return context.Properties["__Tfs_TestConfigurationName__"].ToString().ToLower();
            }
            else
            {
                return ConfigurationManager.AppSettings["DefaultBrowser"];
            }

        }

    }
}

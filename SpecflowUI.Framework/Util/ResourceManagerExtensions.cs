using System;
using System.Resources;
using System.Collections.Generic;

namespace SpecflowUI.Framework.Util
{
    public static class ResourceManagerExtensions
    {
        public static string GetStringFromResource(this ResourceManager resourceManager, string resourceContentName)
        {

            var a = System.Threading.Thread.CurrentThread.CurrentUICulture;
            var b = System.Threading.Thread.CurrentThread.CurrentCulture;

            var expectedValue = resourceManager.GetString(resourceContentName);


            if (String.IsNullOrWhiteSpace(expectedValue))
            {
                throw new InvalidOperationException(string.Format("Resource file does not contain {0}", resourceContentName));
            }
            return expectedValue.Trim();
        }

        public static string GetPageTitleFromResource(this ResourceManager resourceManager, string resourceContentName)
        {
            return GetStringFromResource(resourceManager, resourceContentName + "_Title");
        }

        public static string GetUniqueStringFromResource(this ResourceManager resourceManager, string resourceContentName)
        {

            return GetStringFromResource(resourceManager, resourceContentName + "_UniqueText");
        }
    }
}

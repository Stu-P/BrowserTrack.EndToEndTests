using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using System.Configuration;
using System.Globalization;
using System.Threading;
using SpecflowUI.Framework.Util;
using System.Reflection;
using OpenQA.Selenium.Remote;
using System.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using SpecResults;
using SpecResults.Model;
using RelevantCodes.ExtentReports;
using SpecResults.ExtentReporter;
using SpecResults.WebApp;

namespace SpecflowUI.Framework
{
    [Binding]
    public class FeatureBase : TestClassBase
    {


        /// <summary>
        /// Sets the Current page to the specified value - provided to help readability
        /// </summary>
        protected static PageBase NextPage { set { CurrentPage = value; } }



        protected static PageBase CurrentPage
        {

            get { return (PageBase)ScenarioContext.Current["CurrentPage"]; }
            set { ScenarioContext.Current["CurrentPage"] = value; }
        }

        [BeforeScenario]
        public static void BeforeScenario()
        {
            if (!ScenarioContext.Current.ContainsKey("CurrentDriver"))
            {
                ScenarioContext.Current.Add("CurrentDriver", CurrentDriver);
            }
            else
            {
                CurrentDriver = (RemoteWebDriver)ScenarioContext.Current["CurrentDriver"];
            }

            Test_Setup( TestLibraryAssembly);

            if (!ScenarioContext.Current.ContainsKey("PageContentManager"))
            {
                ScenarioContext.Current.Add("PageContentManager", PageContentManager);
            }
            else
            {
                PageContentManager = (ResourceManager)ScenarioContext.Current["PageContentManager"];
            }
        }

        [AfterScenario]
        public static void AfterScenario()
        {
            Test_Teardown();
            if (ScenarioContext.Current.ContainsKey("PageContentManager"))
            {

                ScenarioContext.Current.Remove("PageContentManager");
            }

            if (ScenarioContext.Current.ContainsKey("CurrentDriver"))
            {

                ScenarioContext.Current.Remove("CurrentDriver");
            }
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            Class_Teardown();

        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            ExtentReports extent = new ExtentReports(@"C:\htmlspecflowreport.html", true);
            

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["HTMLReport"]))
            {
                //var webApp = new WebAppReporter();
                //webApp.Settings.Title = "Specflow UI tests";
                //webApp.Settings.StepDetailsTemplateFile = GetAbsolutePath(@"ReportTemplate\step-details.tpl.html");
                //webApp.Settings.CssFile = GetAbsolutePath(@"ReportTemplate\styles.css");
                //webApp.Settings.DashboardTextFile = GetAbsolutePath(@"ReportTemplate\dashboard-text.md");

                var extentReporter = new ExtentReporter();

                Reporters.Add(extentReporter);

                var screenshotFolder = GetAbsolutePath("screenshots");
                //    //var appFolder = GetAbsolutePath("app");
                //    var screenshotFolder = GetReportPath("screenshots");
                //    var appFolder = GetReportPath("app");

                //    if (Directory.Exists(screenshotFolder))
                //    {
                //        Directory.Delete(screenshotFolder, true);
                //    }

                ExtentTest feature = new ExtentTest("", "");
                ExtentTest scenario = new ExtentTest("", "");

                Reporters.StartedFeature += (sender, args) =>
                {
                    feature = extent.StartTest(args.Feature.Title);
                };

                Reporters.FinishedFeature += (sender, args) =>
                {
                    extent.EndTest(feature);
                };

                Reporters.StartedScenario += (sender, args) =>
                {
                    scenario = new ExtentTest(args.Scenario.Title, "");
                    feature.AppendChild(scenario);
                };


                Reporters.FinishedScenario += (sender, args) =>
                {
                    extent.EndTest(scenario);
                };

                Reporters.FinishedStep += (sender, args) =>
                {

                //  var path = Path.Combine("screenshots", Guid.NewGuid().ToString() + ".png");

                var filename = Guid.NewGuid().ToString() + ".png";
                    var absolutePath = Path.Combine(@"C:\testscreenshots", filename);
                    CurrentDriver.TakeScreenshot(absolutePath);

                    LogStatus logStatus = LogStatus.Unknown;

                    switch (args.Step.Result)
                    {
                        case TestResult.OK:
                            logStatus = LogStatus.Pass; break;
                        case TestResult.Error:
                            logStatus = LogStatus.Fail; break;
                        case TestResult.NotRun:
                            logStatus = LogStatus.Skip; break;
                        case TestResult.Pending:
                            logStatus = LogStatus.Warning; break;
                        case TestResult.Unknown:
                            logStatus = LogStatus.Unknown; break;
                    }


                    string stepType = args.ScenarioBlock.BlockType.ToString();

                    string table = "";

                    if (args.Step.Table != null)
                        table = htmlTable(args.Step.Table);

                    string error = "";
                    if (args.Step.Exception != null)
                        error += args.Step.Exception.ExceptionType + args.Step.Exception.Message;

                    scenario.Log(logStatus, stepType + " " + args.Step.Title + scenario.AddScreenCapture(absolutePath) + table, error);

                };

                Reporters.FinishedReport += (sender, args) =>
                {
                    extent.Flush();
                };
            }

        }
        [AfterStep]
        private static void AfterStep() {

            
        }

        private static string htmlTable(SpecResults.Model.TableParam spectable) {

            string html = "<table><thead><tr>";

            foreach (var header in spectable.Columns) {
                html += "<th>" + header + "</th>";
            }
            html += "</tr></thead><tbody>";

            foreach (var row in spectable.Rows)
            {
                html += "<tr>";
                foreach (var cell in row.ToList()) {
                    html += "<td>" + cell.Value +"</td>"; 
                }

                html += "</tr>";
            }

            html += "</tbody></table>"; 

                return html;
        }

        private static string GetAbsolutePath(string path)
        {

            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);

        }

        private static string GetReportPath(string path)
        {
            var basePath = ConfigurationManager.AppSettings["ReportPath"].ToString();
            return Path.Combine(basePath, path);

        }


        public static Assembly TestLibraryAssembly
        {
            get
            {
                return (Assembly)FeatureContext.Current["TestLibraryAssembly"];
            }
        }
    }
}
using RelevantCodes.ExtentReports;
using SpecResults.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecResults.ExtentReporter
{

    public class ExtentReporter : Reporter
    {


        public ExtentReporter()
        {

            //ExtentReports extent = new ExtentReports(@"C:\htmlspecflowreport.html", true);

            //ExtentTest feature = new ExtentTest("", "");
            //ExtentTest scenario = new ExtentTest("", "");

            //Reporters.StartedFeature += (sender, args) =>
            //{
            //    feature = extent.StartTest(args.Feature.Title);
            //};

            //Reporters.FinishedFeature += (sender, args) =>
            //{
            //    extent.EndTest(feature);
            //};

            //Reporters.StartedScenario += (sender, args) =>
            //{
            //    scenario = new ExtentTest(args.Scenario.Title, "");
            //    feature.AppendChild(scenario);
            //};


            //Reporters.FinishedScenario += (sender, args) =>
            //{
            //    extent.EndTest(scenario);
            //};

            //Reporters.FinishedStep += (sender, args) =>
            //{

            //    //  var path = Path.Combine("screenshots", Guid.NewGuid().ToString() + ".png");

            //    var filename = Guid.NewGuid().ToString() + ".png";
            //    var absolutePath = Path.Combine(@"C:\testscreenshots", filename);
            //   // CurrentDriver.TakeScreenshot(absolutePath);

            //    LogStatus logStatus = LogStatus.Unknown;

            //    switch (args.Step.Result)
            //    {
            //        case TestResult.OK:
            //            logStatus = LogStatus.Pass; break;
            //        case TestResult.Error:
            //            logStatus = LogStatus.Fail; break;
            //        case TestResult.NotRun:
            //            logStatus = LogStatus.Skip; break;
            //        case TestResult.Pending:
            //            logStatus = LogStatus.Warning; break;
            //        case TestResult.Unknown:
            //            logStatus = LogStatus.Unknown; break;
            //    }


            //    string stepType = args.ScenarioBlock.BlockType.ToString();

            //    string table = "";

            //    if (args.Step.Table != null)
            //        table = htmlTable(args.Step.Table);

            //    string error = "";
            //    if (args.Step.Exception != null)
            //        error += args.Step.Exception.ExceptionType + args.Step.Exception.Message;

            //    scenario.Log(logStatus, stepType + " " + args.Step.Title + scenario.AddScreenCapture(absolutePath) + table, error);

            //};

            //Reporters.FinishedReport += (sender, args) =>
            //{
            //    extent.Flush();
            //};
        }
        public override void WriteToStream(Stream stream)
        {
            
        }

        private static string htmlTable(SpecResults.Model.TableParam spectable)
        {

            string html = "<table><thead><tr>";

            foreach (var header in spectable.Columns)
            {
                html += "<th>" + header + "</th>";
            }
            html += "</tr></thead><tbody>";

            foreach (var row in spectable.Rows)
            {
                html += "<tr>";
                foreach (var cell in row.ToList())
                {
                    html += "<td>" + cell.Value + "</td>";
                }

                html += "</tr>";
            }

            html += "</tbody></table>";

            return html;
        }

    }
}

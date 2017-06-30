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

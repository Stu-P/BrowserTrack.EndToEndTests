using System.Collections.Generic;
using System.Linq;

namespace SpecResults.Model
{
	public class Scenario : TaggedReportItem
	{
	//	public ScenarioBlock Given { get; set; }
        public List<ScenarioBlock> ScenarioBlocks { get; set; }


		public override TestResult Result
		{
            
            get {

                List<TestResult> all = new List<TestResult>();


                foreach (var block in ScenarioBlocks)
                {
                    all.Add(block.Result);
                }

                return all.GetResult();

            }
		}
	}
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecResults.Model
{


    public class WhenThenBlock
    {
        public ScenarioBlock When { get; set; }
        public ScenarioBlock Then { get; set; }

        //public List<TestResult> Result
        //{
        //    get { return new List<TestResult> { When.Result, Then.Result }; }
        //}
    }

}
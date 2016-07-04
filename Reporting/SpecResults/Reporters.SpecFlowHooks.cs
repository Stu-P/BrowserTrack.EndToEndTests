using System.Collections.Generic;
using System.Linq;
using SpecResults.Model;
using TechTalk.SpecFlow;
using ScenarioBlock = SpecResults.Model.ScenarioBlock;

namespace SpecResults
{
	public static partial class Reporters
	{
		private static bool _testrunIsFirstFeature;

		[AfterFeature]
        public static void AfterFeature()
		{
			foreach (var reporter in reporters)
			{
				var feature = reporter.CurrentFeature;

				var scenarioOutlineGroups = feature.Scenarios.GroupBy(scenario => scenario.Title)
					.Where((scenarioGrp, key) => scenarioGrp.Count() > 1)
					.Select((scenarioGrp, key) => scenarioGrp.ToList());

				foreach (var scenarioOutlineGroup in scenarioOutlineGroups)
				{
					for (int i = 0; i < scenarioOutlineGroup.Count(); i++)
					{
						scenarioOutlineGroup[i].Title = string.Format("{0} (example {1})", scenarioOutlineGroup[i].Title, i + 1);
					}
				}

				feature.EndTime = CurrentRunTime;
				OnFinishedFeature(reporter);
				reporter.CurrentFeature = null;
			}
		}

		[AfterScenario]
        public static void AfterScenario()
		{
			foreach (var reporter in reporters.ToArray())
			{
				var scenario = reporter.CurrentScenario;
				scenario.EndTime = CurrentRunTime;
				OnFinishedScenario(reporter);
				reporter.CurrentScenario = null;
			}
		}

		[AfterScenarioBlock]
		internal static void AfterScenarioBlock()
		{
			var endtime = CurrentRunTime;
			foreach (var reporter in reporters)
			{
				var scenarioblock = reporter.CurrentScenarioBlock;
				scenarioblock.EndTime = endtime;
				OnFinishedScenarioBlock(reporter);
				reporter.CurrentScenarioBlock = null;
			}
		}

		[AfterTestRun]
		internal static void AfterTestRun()
		{
			foreach (var reporter in reporters)
			{
				reporter.Report.EndTime = CurrentRunTime;
				OnFinishedReport(reporter);
			}
		}

		[BeforeFeature]
        public static void BeforeFeature()
		{
			var starttime = CurrentRunTime;

			// Init reports when the first feature runs. This is intentionally not done in
			// BeforeTestRun(), to make sure other [BeforeTestRun] annotated methods can perform
			// initialization before the reports are created.
			if (_testrunIsFirstFeature)
			{
				foreach (var reporter in reporters)
				{
					reporter.Report = new Report
					{
						Features = new List<Feature>(),
						Generator = reporter.Name,
						StartTime = starttime
					};

					OnStartedReport(reporter);
				}

				_testrunIsFirstFeature = false;
			}

			foreach (var reporter in reporters)
			{
				var feature = new Feature
				{
					Tags = new List<string>(FeatureContext.Current.FeatureInfo.Tags),
					Scenarios = new List<Scenario>(),
					StartTime = starttime,
					Title = FeatureContext.Current.FeatureInfo.Title,
					Description = FeatureContext.Current.FeatureInfo.Description
				};

				reporter.Report.Features.Add(feature);
				reporter.CurrentFeature = feature;

				OnStartedFeature(reporter);
			}
		}

		[BeforeScenario]
		public static void BeforeScenario()
		{
			var starttime = CurrentRunTime;

			foreach (var reporter in reporters)
			{
				var scenario = new Scenario
				{
					Tags = new List<string>(ScenarioContext.Current.ScenarioInfo.Tags),
					 ScenarioBlocks = new List<ScenarioBlock>(),
					StartTime = starttime,
					Title = ScenarioContext.Current.ScenarioInfo.Title
				};

				reporter.CurrentFeature.Scenarios.Add(scenario);
				reporter.CurrentScenario = scenario;

				OnStartedScenario(reporter);
			}
		}

		[BeforeScenarioBlock]
		internal static void BeforeScenarioBlock()
		{
			var starttime = CurrentRunTime;

			foreach (var reporter in reporters)
			{
				switch (ScenarioContext.Current.CurrentScenarioBlock)
				{
					case TechTalk.SpecFlow.ScenarioBlock.Given:
                        reporter.CurrentScenario.ScenarioBlocks.Add(new ScenarioBlock { BlockType = BlockTypeEnum.Given, Steps = new List<Step>() });
                        reporter.CurrentScenarioBlock = reporter.CurrentScenario.ScenarioBlocks.Last();
						break;
					case TechTalk.SpecFlow.ScenarioBlock.Then:
                        reporter.CurrentScenario.ScenarioBlocks.Add(new ScenarioBlock { BlockType = BlockTypeEnum.Then, Steps = new List<Step>() });
                        reporter.CurrentScenarioBlock = reporter.CurrentScenario.ScenarioBlocks.Last();
						break;
					case TechTalk.SpecFlow.ScenarioBlock.When:
                        reporter.CurrentScenario.ScenarioBlocks.Add(new ScenarioBlock { BlockType = BlockTypeEnum.When, Steps = new List<Step>() });
                        reporter.CurrentScenarioBlock = reporter.CurrentScenario.ScenarioBlocks.Last();
						break;
				}

				reporter.CurrentScenarioBlock.StartTime = starttime;
				OnStartedScenarioBlock(reporter);
			}
		}

		[BeforeTestRun]
		internal static void BeforeTestRun()
		{
			_testrunIsFirstFeature = true;
		}
	}
}
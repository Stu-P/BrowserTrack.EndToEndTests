(function() {
	"use strict";

	angular.module("app").factory("Report", [
		function($scope, $routeParams) {
			function Report(reportData) {
				this.reportData = reportData;

				// Create a grouped property for each report item type
				this.features = reportData.features;
				this.scenarios = _.flatten(reportData.features, "scenarios");
				this.scenarioBlocks = _.reduce(this.scenarios, function (result, scenario) {
				    _.forEach(scenario.scenario_blocks, function (block, index) {
				        result.push(block);
				    }
                     );
				    return result;
				}, []);
				this.steps = _.reduce(this.scenarioBlocks, function (result, scenarioBlock) {
				    result.push.apply(result, scenarioBlock.steps);
				    return result;
				}, []);

				this.reportItems = _.union(
					this.features,
					this.scenarios,
					this.scenarioBlocks,
					this.steps
				);

				// Decorate features
				_.forEach(this.reportData.features, function(feature, index) {
					feature.id = generateId([feature.title]);
					feature.duration = getDuration(feature);
					feature.type = "feature";

					// Decorate scenarios
					_.forEach(feature.scenarios, function(scenario, index) {
						scenario.id = generateId([feature.title, scenario.title]);
						scenario.duration = getDuration(scenario);
						scenario.featureId = feature.id;
						scenario.type = "scenario";

						var stepNumber = 1;

						// Decorate scenario blocks
						_.forEach(scenario.scenario_blocks, function (scenarioBlock, key) {
						    scenarioBlock.id = generateId([feature.title, scenario.title, scenarioBlock.block_type]);
						    scenarioBlock.duration = getDuration(scenarioBlock);
						    scenarioBlock.featureId = feature.id;
						    scenarioBlock.scenarioId = scenario.id;
						    scenarioBlock.type = "scenarioBlock";
						    _.forEach(scenarioBlock.steps, function (step, index) {
						        step.id = generateId([feature.title, scenario.title, scenarioBlock.block_type, index]);
						        step.duration = getDuration(step);
						        step.featureId = feature.id;
						        step.scenarioId = scenario.id;
						        step.scenarioBlockId = scenarioBlock.id;
						        step.number = stepNumber++;
						        step.type = "step";
						        if (step.exception) {
						            scenario.exception = step.exception;
						            scenarioBlock.exception = step.exception;
						        }
						    });
						});
					});
				});

				// Build search index
				this.searchIndex = _.map(this.scenarios, function(scenario) {
					var feature = this.findFeatureById(scenario.featureId);
					var scenarioblockMap = scenario.scenarioBlocks;

					return {
						feature: {
							id: feature.id,
							title: feature.title,
							description: feature.description,
							tags: feature.tags
						},
						scenario: {
							id: scenario.id,
							title: scenario.title,
							tags: scenario.tags,
							result: scenario.result
						},
						steps: _.reduce(scenario.scenario_blocks, function (result, block) {
						    return result = result.concat(block.steps);
						}, [])
					};
				}, this);
			}

			function getDuration(item) {
				return new Date(item.end_time).getTime() - new Date(item.start_time).getTime();
			}

			function generateId(values) {
				var str = values.join("#####");
				return md5(str);
			};

			Report.prototype = {
				constructor: Report,

				findFeatureById: function(id) {
					return _.find(this.features, function(feature) {
						return feature.id === id;
					});
				},

				findScenarioById: function(id) {
					return _.find(this.scenarios, function(scenario) {
						return scenario.id === id;
					});
				},

				findStepById: function(id) {
					return _.find(this.steps, function(step) {
						return scenario.id === id;
					});
				}
			};


			return Report;
		}
	]);
})();
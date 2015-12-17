using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using System.Collections.Generic;
using System.Globalization;

namespace Stag.ProcessPipeline
{
    public class BuildAgent
    {
        private readonly SolutionConfiguration _solutionConfiguration;

        public BuildAgent()
            : this(SolutionConfiguration.Default)
        {
        }

        public BuildAgent(SolutionConfiguration solutionConfiguration)
        {
            _solutionConfiguration = solutionConfiguration;
        }

        public bool RebuildSolution()
        {
            var solutionFullPath = string.Format(CultureInfo.InvariantCulture, "{0}{1}", _solutionConfiguration.SolutionPath, _solutionConfiguration.SolutionFileName);

            var globalProperties = new Dictionary<string, string>();
            globalProperties.Add("Configuration", "Debug");
            globalProperties.Add("Platform", "Any CPU");

            using (var projects = new ProjectCollection())
            {
                var buildRequest = new BuildRequestData(solutionFullPath, globalProperties, null, new string[] { "Clean", "Build" }, null);

                var buildParameters = new BuildParameters(projects);
                buildParameters.DetailedSummary = true;
                buildParameters.Culture = new System.Globalization.CultureInfo("en-US");
                buildParameters.LogInitialPropertiesAndItems = true;
                var buildResult = BuildManager.DefaultBuildManager.Build(buildParameters, buildRequest);

                return buildResult.OverallResult == BuildResultCode.Success;
            }
        }
    }
}
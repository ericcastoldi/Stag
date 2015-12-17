using System.Globalization;

namespace Stag.ProcessPipeline
{
    public class TestConfiguration
    {
        private const string VSTEST_ARGS = "/logger:trx /inIsolation";

        private const string VSTEST_DEFAULT_PATH = "C:\\Program Files (x86)\\Microsoft Visual Studio 11.0\\Common7\\IDE\\CommonExtensions\\Microsoft\\TestWindow\\vstest.console.exe";

        public TestConfiguration(string testProjectName, string testConfigFileName)
            : this(testProjectName, testConfigFileName, VSTEST_DEFAULT_PATH, SolutionConfiguration.Default)
        {
        }

        public TestConfiguration(string testProjectName, string testConfigFileName, string vsTestPath, SolutionConfiguration solutionConfiguration)
        {
            this.TestProjectName = testProjectName;
            this.TestConfigurationFileName = testConfigFileName;
            this.VsTestPath = vsTestPath;
            this.SolutionConfiguration = solutionConfiguration;
        }

        public SolutionConfiguration SolutionConfiguration { get; set; }

        public string VsTestPath { get; set; }

        public string TestProjectName { get; private set; }

        public string TestConfigurationFileName { get; private set; }

        public RunInfo CreateRunInfo()
        {
            var command = this.VsTestPath;
            var args = string.Format(CultureInfo.InvariantCulture, "{0}{1}\\bin\\Debug\\{1}.dll /Settings:{0}{1}\\{2} {3}", this.SolutionConfiguration.SolutionPath, this.TestProjectName, this.TestConfigurationFileName, VSTEST_ARGS);

            return new RunInfo(command, args);
        }

        public static TestConfiguration Processos
        {
            get
            {
                return new TestConfiguration("ProcessosTeste", "Processos.runsettings");
            }
        }

        public static TestConfiguration Monitor
        {
            get
            {
                return new TestConfiguration("MonitorTeste", "Monitor.runsettings");
            }
        }
    }
}
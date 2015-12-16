namespace Stag.Build
{
    public class SolutionConfiguration
    {
        public SolutionConfiguration(string solutionPath, string solutionFileName)
        {
            this.SolutionPath = solutionPath;
            this.SolutionFileName = solutionFileName;
        }

        public string SolutionPath { get; private set; }

        public string SolutionFileName { get; private set; }

        public static SolutionConfiguration Default
        {
            get
            {
                return new SolutionConfiguration("C:\\git\\os\\",
                                                "SapiensNfe.sln");
            }
        }
    }
}
using System;
using System.Diagnostics;

namespace Stag.Build
{
    public class SimpleProcessRun : IProcessRun
    {
        private IOutputParser _standardOutputParser;
        private IOutputParser _standardErrorParser;

        public SimpleProcessRun(IOutputParser standardOutputParser, IOutputParser standardErrorParser)
        {
            _standardOutputParser = standardOutputParser;
            _standardErrorParser = standardErrorParser;
        }

        public Summary Run(RunInfo buildInfo)
        {
            if (buildInfo == null)
                throw new ArgumentNullException("buildInfo");

            if (string.IsNullOrWhiteSpace(buildInfo.Command))
                throw new ArgumentException("O BuildInfo deve ter um comando válido.");

            var processInfo = new ProcessStartInfo()
            {
                FileName = buildInfo.Command,
                Arguments = buildInfo.Args,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
            };

            var msbuild = new Process() { StartInfo = processInfo };
            msbuild.Start();

            var stdout = msbuild.StandardOutput.ReadToEnd();
            var stderr = msbuild.StandardError.ReadToEnd();

            var standardErrorOut = _standardErrorParser.Parse(stderr);
            if (!standardErrorOut.Success)
            {
                return standardErrorOut;
            }

            return _standardOutputParser.Parse(stdout);
        }
    }
}
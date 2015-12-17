using System;
using System.Diagnostics;

namespace Stag.Build
{
    public class SimpleProcessRun : IProcessRun
    {
        private readonly IOutputParser _standardOutputParser;
        private readonly IOutputParser _standardErrorParser;

        public SimpleProcessRun(IOutputParser standardOutputParser, IOutputParser standardErrorParser)
        {
            _standardOutputParser = standardOutputParser;
            _standardErrorParser = standardErrorParser;
        }

        public Summary Run(RunInfo runInfo)
        {
            if (runInfo == null)
                throw new ArgumentNullException("runInfo");

            if (string.IsNullOrWhiteSpace(runInfo.Command))
                throw new ArgumentException("O RunInfo deve ter um comando válido.");

            using (var msbuild = new Process())
            {
                var processInfo = new ProcessStartInfo()
                {
                    FileName = runInfo.Command,
                    Arguments = runInfo.Args,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                };

                msbuild.StartInfo = processInfo;
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
}
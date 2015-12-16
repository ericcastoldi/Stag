using System;
using System.Diagnostics;

namespace Stag.Build
{
    public class AsyncProcessRun : IProcessRun
    {
        private readonly IAsyncOutputParser _standardOutputParser;
        private readonly IAsyncOutputParser _standardErrorParser;

        public AsyncProcessRun(IAsyncOutputParser standardOutputParser, IAsyncOutputParser standardErrorParser)
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

            var proc = new Process() { StartInfo = processInfo };

            proc.OutputDataReceived += (sender, eventArgs) => { _standardOutputParser.Parse(eventArgs.Data); };
            proc.ErrorDataReceived += (sender, eventArgs) => { _standardErrorParser.Parse(eventArgs.Data); };

            proc.Start();
            proc.BeginErrorReadLine();
            proc.BeginOutputReadLine();
            proc.WaitForExit();

            if (!_standardErrorParser.OverallSuccess)
            {
                return _standardErrorParser.OverallResult;
            }

            return _standardOutputParser.OverallResult;
        }
    }
}
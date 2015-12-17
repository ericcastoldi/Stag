using System;
using System.Diagnostics;

namespace Stag.ProcessPipeline
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

        public Summary Run(RunInfo runInfo)
        {
            if (runInfo == null)
                throw new ArgumentNullException("runInfo");

            if (string.IsNullOrWhiteSpace(runInfo.Command))
                throw new ArgumentException("O RunInfo deve ter um comando válido.");

            using (var proc = new Process())
            {
                proc.StartInfo = CreateProcessStartInfo(runInfo);

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

        private static ProcessStartInfo CreateProcessStartInfo(RunInfo runInfo)
        {
            return new ProcessStartInfo()
            {
                FileName = runInfo.Command,
                Arguments = runInfo.Args,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
            };
        }
    }
}
using System;

namespace Stag.ProcessPipeline
{
    public class TestOutputParser : AsyncOutputParserBase
    {
        public override Summary Parse(string output)
        {
            var summary = new Summary(output, true);

            if (!string.IsNullOrWhiteSpace(output) && output.StartsWith("Failed", StringComparison.OrdinalIgnoreCase))
            {
                summary = new Summary(output, false);
            }

            this.Results.Add(summary);
            return summary;
        }
    }
}
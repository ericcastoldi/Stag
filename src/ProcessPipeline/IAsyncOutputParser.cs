using System.Collections.Generic;

namespace Stag.ProcessPipeline
{
    public interface IAsyncOutputParser : IOutputParser
    {
        bool OverallSuccess { get; }

        Summary OverallResult { get; }

        IList<Summary> Results { get; }
    }
}
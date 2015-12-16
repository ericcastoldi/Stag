using System.Collections.Generic;

namespace Stag.Build
{
    public interface IAsyncOutputParser : IOutputParser
    {
        bool OverallSuccess { get; }

        Summary OverallResult { get; }

        IList<Summary> Results { get; }
    }
}
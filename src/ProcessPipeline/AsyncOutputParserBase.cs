using System;
using System.Collections.Generic;
using System.Linq;

namespace Stag.ProcessPipeline
{
    public abstract class AsyncOutputParserBase : IAsyncOutputParser
    {
        private readonly List<Summary> _summaries;

        protected AsyncOutputParserBase()
        {
            _summaries = new List<Summary>();
        }

        public abstract Summary Parse(string output);

        public bool OverallSuccess
        {
            get { return _summaries.All(p => p.Success); }
        }

        public Summary OverallResult
        {
            get
            {
                var messages = string.Join(Environment.NewLine, _summaries.Select(p => p.Output).ToArray());
                return new Summary(messages, this.OverallSuccess);
            }
        }

        public IList<Summary> Results
        {
            get { return _summaries; }
        }
    }
}
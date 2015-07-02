using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stag.SourceControl
{
    public class GitException : InvalidOperationException
    {
        public GitException(string shortMessage, string longMessage = null)
        {
            ShortMessage = shortMessage;
            LongMessage = longMessage;
        }

        public string ShortMessage { get; set; }

        public string LongMessage { get; set; }
    }
}

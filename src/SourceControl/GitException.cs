using System;
using System.Runtime.Serialization;

namespace Stag.SourceControl
{
    [Serializable]
    public class GitException : InvalidOperationException
    {
        public GitException(string shortMessage, Exception innerException)
            : base(shortMessage, innerException)
        {
        }

        public GitException()
            : base()
        {
        }

        protected GitException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        public GitException(string shortMessage)
            : this(shortMessage, (string)null)
        {
        }

        public GitException(string shortMessage, string longMessage)
            : base(shortMessage)
        {
            ShortMessage = shortMessage;
            LongMessage = longMessage;
        }

        public string ShortMessage { get; set; }

        public string LongMessage { get; set; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("ShortMessage", ShortMessage);
            info.AddValue("LongMessage", LongMessage);

            base.GetObjectData(info, context);
        }
    }
}
namespace Stag.Build
{
    public class AsyncStandardErrorParser : AsyncOutputParserBase
    {
        private readonly IOutputParser _standardErrorParser;

        public AsyncStandardErrorParser()
            : this(new StandardErrorParser())
        {
        }

        public AsyncStandardErrorParser(IOutputParser standardErrorParser)
        {
            _standardErrorParser = standardErrorParser;
        }

        public override Summary Parse(string output)
        {
            var summary = _standardErrorParser.Parse(output);
            this.Results.Add(summary);

            return summary;
        }
    }
}
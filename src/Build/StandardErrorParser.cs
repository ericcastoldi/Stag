namespace Stag.Build
{
    public class StandardErrorParser : IOutputParser
    {
        public Summary Parse(string output)
        {
            if (!string.IsNullOrWhiteSpace(output))
            {
                return new Summary(output, false);
            }

            return new Summary(string.Empty, true);
        }
    }
}
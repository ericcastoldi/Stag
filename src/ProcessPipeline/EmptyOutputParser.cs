namespace Stag.ProcessPipeline
{
    internal class EmptyOutputParser : IOutputParser
    {
        public Summary Parse(string output)
        {
            return
              new Summary(output, true);
        }
    }
}
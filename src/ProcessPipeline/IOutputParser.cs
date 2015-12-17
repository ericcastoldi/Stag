namespace Stag.ProcessPipeline
{
    public interface IOutputParser
    {
        Summary Parse(string output);
    }
}
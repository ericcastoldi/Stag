namespace Stag.Build
{
    public interface IOutputParser
    {
        Summary Parse(string output);
    }
}
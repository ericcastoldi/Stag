namespace Stag.Build
{
    public interface IProcessRun
    {
        Summary Run(RunInfo buildInfo);
    }
}
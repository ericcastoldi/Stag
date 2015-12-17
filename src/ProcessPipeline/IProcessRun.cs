namespace Stag.ProcessPipeline
{
    public interface IProcessRun
    {
        Summary Run(RunInfo runInfo);
    }
}
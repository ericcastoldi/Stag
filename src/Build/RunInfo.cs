namespace Stag.Build
{
    public class RunInfo
    {
        public RunInfo(string command, string args)
        {
            this.Args = args;
            this.Command = command;
        }

        public string Command { get; private set; }

        public string Args { get; private set; }
    }
}
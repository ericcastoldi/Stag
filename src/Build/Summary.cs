namespace Stag.Build
{
    public class Summary
    {
        private readonly bool _success;
        private readonly string _output;

        public Summary(string output, bool success)
        {
            _output = output;
            _success = success;
        }

        public bool Success
        {
            get { return _success; }
        }

        public string Output
        {
            get { return _output; }
        }
    }
}
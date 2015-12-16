using FX.Configuration;

namespace Stag.Configuration
{
    internal class Settings : JsonConfiguration, ISettings
    {
        public string Workspace { get; set; }

        public string WorkBranch { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string RemoteUrl { get; set; }

        public string TaskBranchPrefix { get; set; }

        public string MergeBranchPrefix { get; set; }

        public string GsbdBranchPrefix { get; set; }

        public string GitLabPrivateToken { get; set; }

        public string StorageBasePath { get; set; }

        public string MiscTaskId { get; set; }

        public string GitLabProjectName { get; set; }
    }
}
using System;

namespace Stag.Configuration
{
    internal interface ISettings
    {
        string Email { get; set; }
        string GitLabPrivateToken { get; set; }
        string GitLabProjectName { get; set; }
        string GsbdBranchPrefix { get; set; }
        string MergeBranchPrefix { get; set; }
        string MiscTaskId { get; set; }
        string Password { get; set; }
        string RemoteUrl { get; set; }
        string StorageBasePath { get; set; }
        string TaskBranchPrefix { get; set; }
        string Username { get; set; }
        string WorkBranch { get; set; }
        string Workspace { get; set; }
    }
}

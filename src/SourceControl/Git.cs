using LibGit2Sharp;
using Stag.Configuration;
using System;
using System.IO;

namespace Stag.SourceControl
{
    public class Git
    {
        private Settings _settings;

        public Git()
        {
            _settings = new Settings();
        }

        public void Clone(string repo, string branchName)
        {
            var path = Path.Combine(_settings.StorageBasePath, "workspace");
            Repository.Clone(repo, path, new CloneOptions() { BranchName = branchName, CredentialsProvider = BuildCredentials });
        }

        public void Merge(string targetBranch)
        {
            using (var repo = new Repository(_settings.Workspace))
            {
                repo.Merge(repo.Branches[targetBranch].Tip, new Signature(_settings.Username, _settings.Email, DateTimeOffset.Now));
            }
        }

        public Branch CreateBranch(string branch)
        {
            return CreateBranch(branch, _settings.WorkBranch);
        }

        public Branch CreateBranch(string branch, string baseBranchName)
        {
            using (var repo = new Repository(_settings.Workspace))
            {
                var baseBranch = repo.Lookup<Commit>(baseBranchName);
                return repo.CreateBranch(branch, baseBranch);
            }
        }

        public Branch Checkout()
        {
            return Checkout(_settings.WorkBranch);
        }

        public Branch Checkout(string branch)
        {
            using (var repo = new Repository(_settings.Workspace))
            {
                return repo.Checkout(branch);
            }
        }

        public void DeleteBranch(string branch)
        {
            using (var repo = new Repository(_settings.Workspace))
            {
                repo.Branches.Remove(branch);
            }
        }

        public MergeResult UpdateCurrentBranch()
        {
            using (var repo = new Repository(_settings.Workspace))
            {
                var pullOptions = new PullOptions() { FetchOptions = new FetchOptions() { CredentialsProvider = BuildCredentials } };
                return repo.Network.Pull(new Signature(_settings.Username, _settings.Email, DateTimeOffset.Now), pullOptions);
            }
        }

        public void Push(string branchName)
        {
            using (var repo = new Repository(_settings.Workspace))
            {
                var pushOptions = new PushOptions() { CredentialsProvider = BuildCredentials };
                repo.Network.Push(repo.Branches[branchName], pushOptions);
            }
        }

        private Credentials BuildCredentials(string url, string usernameFromUrl, SupportedCredentialTypes types)
        {
            return new UsernamePasswordCredentials() { Username = _settings.Username, Password = _settings.Password };
        }
    }
}
using LibGit2Sharp;
using Stag.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stag.SourceControl
{
    public class LiteCommit
    {
        public string Author { get; set; }
        public string Message { get; set; }
        public string Changes { get; set; }
    }

    public class Git
    {
        private ISettings _settings;

        public Git()
            : this(new Settings())
        {
        }

        internal Git(ISettings settings)
        {
            _settings = settings;
        }

        public void HardReset()
        {
            using (var repo = new Repository(_settings.Workspace))
            {
                repo.Reset(ResetMode.Hard);
            }
        }

        public IList<LiteCommit> GetCommits()
        {
            using (var repo = new Repository(_settings.Workspace))
            {
                var commits = GetMasterCommitsRaw(repo);

                var liteCommits =
                            commits
                                .Select(p => new LiteCommit()
                                {
                                    Author = string.Format("[Author: {0}({1}) | Commiter: {2}({3})]", p.Author.Name, p.Author.Email, p.Committer.Name, p.Committer.Email),
                                    Message = p.Message,
                                    Changes = CompareTree(repo, p)
                                })
                            .ToList();

                return liteCommits;
            }
        }

        public string CompareTree(Repository repo, Commit commit)
        {
            Tree commitTree = commit.Tree;

            if (commit.Parents.Count() > 1)
            {
                return "Mais de um parent";
            }

            Tree parentCommitTree = commit.Parents.Single().Tree; // Secondary Tree
            var patch = repo.Diff.Compare<Patch>(parentCommitTree, commitTree); // Difference

            var sb = new StringBuilder();
            foreach (var ptc in patch)
            {
                sb.AppendLine(ptc.Status + " -> " + ptc.Path); // Status -> File Path
            }

            return sb.ToString();
        }

        public IList<Commit> GetMasterCommitsRaw(Repository repo)
        {
            var master = repo.Branches["master"];

            var commits = master
                    .Commits
                    .Where(p => !p.Message.StartsWith("Merge branch '")
                             && !p.Message.StartsWith("Merge commit '")
                             && !(p.Author.Email == "joao.maas@senior.com.br" || p.Committer.Email == "joao.maas@senior.com.br"))
                    .ToList();

            var firstTree = commits[0].Tree;

            return commits;
        }

        public IList<LiteCommit> GetMasterCommits()
        {
            using (var repo = new Repository(_settings.Workspace))
            {
                var master = repo.Branches["master"];

                var commits = master
                        .Commits
                        .Where(p => !p.Message.StartsWith("Merge branch '")
                                 && !p.Message.StartsWith("Merge commit '")
                                 && !(p.Author.Email == "joao.maas@senior.com.br" || p.Committer.Email == "joao.maas@senior.com.br"))
                        .ToList(); ;

                var liteCommits = new List<LiteCommit>();
                foreach (var commit in commits)
                {
                    var cmm = new LiteCommit() { Author = commit.Author.Email, Message = commit.MessageShort };

                    Tree commitTree = commit.Tree;
                    Tree parentCommitTree = commit.Parents.Single().Tree; // Secondary Tree
                    var patch = repo.Diff.Compare<Patch>(parentCommitTree, commitTree); // Difference

                    var sb = new StringBuilder();
                    foreach (var ptc in patch)
                    {
                        sb.AppendLine(ptc.Status + " -> " + ptc.Path); // Status -> File Path
                    }

                    cmm.Changes = sb.ToString();
                    liteCommits.Add(cmm);
                }

                return liteCommits;
                //return commits
                //    .Select(p => new LiteCommit()
                //    {
                //        Author = string.Format("[Author: {0}({1}) | Commiter: {2}({3})]", p.Author.Name, p.Author.Email, p.Committer.Name, p.Committer.Email),
                //        Message = p.Message,
                //    })
                //    .ToList();
            }
        }

        public string CompareTrees()
        {
            using (var repo = new Repository(_settings.Workspace))
            {
                Tree commitTree = repo.Head.Tip.Tree; // Main Tree
                Tree parentCommitTree = repo.Head.Tip.Parents.Single().Tree; // Secondary Tree

                var patch = repo.Diff.Compare<Patch>(parentCommitTree, commitTree); // Difference

                var sb = new StringBuilder();
                foreach (var ptc in patch)
                {
                    sb.AppendLine(ptc.Status + " -> " + ptc.Path); // Status -> File Path
                }

                return sb.ToString();
            }
        }

        public IList<Branch> GetAllBranches()
        {
            using (var repo = new Repository(_settings.Workspace))
            {
                return repo.Branches.ToList();
            }
        }

        public Branch GetCurrentBranch()
        {
            using (var repo = new Repository(_settings.Workspace))
            {
                return repo.Head;
            }
        }

        public void Clone(string repo, string branchName)
        {
            Repository.Clone(repo, _settings.Workspace, new CloneOptions() { BranchName = branchName, CredentialsProvider = BuildCredentials });
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
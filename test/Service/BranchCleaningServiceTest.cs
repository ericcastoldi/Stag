using Moq;
using Stag.Configuration;
using Stag.Service;
using Stag.SourceControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Stag.Test.Service
{
    public class BranchCleaningServiceTest
    {
        //[Fact]
        public void ShouldGetCommits()
        {
            var git = new Git();
            var commits = git.GetCommits();

            var log = string.Join(Environment.NewLine, commits.Select(p => string.Format("{0} | {1}{2} {3}", p.Author, p.Message, Environment.NewLine, p.Changes)));

            Assert.NotNull(log);
        }

        //[Fact]
        public void ShouldGetModifications()
        {
            var git = new Git();

            var modifications = git.CompareTrees();

            Assert.NotNull(modifications);
        }

        //[Fact]
        public void ShouldGetMasterCommits()
        {
            var git = new Git();

            var commits = git.GetMasterCommits();

            var commitsString = string.Join(Environment.NewLine,
                commits
                .Where(p => !p.Message.StartsWith("Merge branch") && !p.Message.StartsWith("Merge commit"))
                .Select(p => string.Format("{0}{1}{2}", p.Author, Environment.NewLine, p.Message))
                .ToArray());

            Assert.NotNull(commitsString);
        }

        //[Fact]
        public void ShouldGetBranches()
        {
            // Arrange
            var settingsMock = new Mock<ISettings>();
            settingsMock
                .SetupGet(p => p.Workspace)
                .Returns("C:\\git\\gsbd");

            var cleaningService = new BranchCleaningService(new Git(settingsMock.Object));

            // Act
            var branches = cleaningService.GetNonOficialBranches();

            // Assert
            Assert.NotNull(branches);
        }
    }
}
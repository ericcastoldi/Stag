using Stag.Configuration;
using Stag.Model;
using Stag.Utility;
using System;

namespace Stag.Service
{
    public class BranchNamingService
    {
        private Settings _settings;
        public BranchNamingService()
        {
            _settings = new Settings();
        }

        public string CreateMergeBranchName(Task task)
        {
            if (task == null || string.IsNullOrWhiteSpace(task.DevelopmentBranchName))
            {
                throw new ArgumentNullException("task");
            }

            return this.CreateMergeBranchName(task.DevelopmentBranchName);
        }

        public string CreateMergeBranchName(string developmentBranchName)
        {
            if (string.IsNullOrWhiteSpace(developmentBranchName))
            {
                throw new ArgumentNullException("developmentBranchName");
            }

            if (developmentBranchName.Contains("/"))
            { 
                developmentBranchName = developmentBranchName.Split('/')[1];
            }

            return string.Format("{0}/{1}-para-{2}", _settings.MergeBranchPrefix, developmentBranchName, _settings.WorkBranch);
        }

        public string CreateDevelopmentBranchName(Task task)
        {
            if(task == null 
                || (string.IsNullOrWhiteSpace(task.Id)
                    || string.IsNullOrWhiteSpace(task.Title)))
            {
                throw new ArgumentNullException("task");
            }

            var taskName = string.Format("{0} {1}", task.Id, task.Title);
            return CreateDevelopmentBranchName(taskName.Trim());
        }

        private string CreateDevelopmentBranchName(string taskName)
        {
            return string.Format("{0}/{1}", _settings.TaskBranchPrefix, taskName.GenerateSlug());
        }
    }
}

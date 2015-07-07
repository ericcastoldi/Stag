using Stag.Configuration;
using Stag.Model;
using Stag.Util;

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
            return this.CreateMergeBranchName(task, task.DevelopmentBranchName);
        }

        public string CreateMergeBranchName(Task task, string developmentBranchName)
        {
            if (developmentBranchName.Contains("/"))
            { 
                developmentBranchName = developmentBranchName.Split('/')[1];
            }

            return string.Format("{0}/{1}-para-{2}", _settings.MergeBranchPrefix, developmentBranchName, _settings.WorkBranch);
        }

        public string CreateDevelopmentBranchName(Task task)
        {
            var taskName = string.Format("{0} {1}", task.Id, task.Title);
            return string.Format("{0}/{1}", _settings.TaskBranchPrefix, taskName.GenerateSlug());
        }
    }
}

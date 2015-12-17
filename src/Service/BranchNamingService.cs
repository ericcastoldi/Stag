using Stag.Configuration;
using Stag.Tasks;
using Stag.Utility;
using System;
using System.Globalization;

namespace Stag.Service
{
    public class BranchNamingService
    {
        private Settings _settings;

        public BranchNamingService()
        {
            _settings = new Settings();
        }

        public string CreateDevelopmentBranchName(Task task)
        {
            if (task == null
                || (string.IsNullOrWhiteSpace(task.Id)
                    || string.IsNullOrWhiteSpace(task.Title)))
            {
                throw new ArgumentNullException("task");
            }

            return string.Format(CultureInfo.InvariantCulture, "{0}/{1}", _settings.TaskBranchPrefix, task.ToString().GenerateSlug());
        }
    }
}
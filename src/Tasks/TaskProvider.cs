using System.Collections.Generic;
using System.Linq;

namespace Stag.Tasks
{
    internal class TaskProvider : ITaskProvider
    {
        private readonly ITaskProvider _localProvider;
        private readonly ITaskProvider _helpnetProvider;

        public TaskProvider()
            : this(new HelpnetTaskProvider(), new LocalTaskProvider())
        {
        }

        public TaskProvider(ITaskProvider helpnetProvider, ITaskProvider localProvider)
        {
            _localProvider = localProvider;
            _helpnetProvider = helpnetProvider;
        }

        public IList<Task> GetTasks()
        {
            var localTasks = _localProvider.GetTasks();
            var helpnetTasks = _helpnetProvider.GetTasks();

            foreach (var task in helpnetTasks)
            {
                if (!localTasks.Any(p => p.Id == task.Id))
                {
                    localTasks.Add(task);
                }
            }

            return localTasks.ToList();
        }
    }
}
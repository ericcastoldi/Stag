using System.Collections.Generic;

namespace Stag.Tasks
{
    internal interface ITaskProvider
    {
        IList<Task> GetTasks();
    }
}
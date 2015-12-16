using Stag.Storage;
using System.Collections.Generic;
using System.Linq;

namespace Stag.Tasks
{
    internal class LocalTaskProvider : ITaskProvider
    {
        private readonly IWarehouse<Task> _warehouse;

        public LocalTaskProvider(IWarehouse<Task> warehouse)
        {
            _warehouse = warehouse;
        }

        public IList<Task> GetTasks()
        {
            return _warehouse.Retrieve().ToList();
        }
    }
}
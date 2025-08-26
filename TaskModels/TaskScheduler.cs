using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskModels.Tasks;
using TaskModels.Tasks.Abstract;

namespace TaskModels
{
    public class TaskScheduler
    {
        private List<IShellCommand> _taskQueue;

        public TaskScheduler()
        {
            _taskQueue = new List<IShellCommand>();
        }

        public void AddNewShellTask(IShellCommand newTask)
        {
            _taskQueue.Add(newTask);
        }

        public async Task ExecuteScheduledTasks(CancellationToken cancellationToken)
        {
            foreach (var task in _taskQueue)
            {
                await task.Execute();
            }          
        }

        public async Task StopPeriodicTasks()
        {
            var stopTasks = _taskQueue.OfType<PeriodicTask>().Select(task => task.StopPeriodicExecution());
            Task.WhenAll(stopTasks);
        }
    }
}

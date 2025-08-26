using System;
using System.Threading;
using TaskModels;
using TaskModels.Tasks.Factories;

namespace TaskSchedulerProject
{
    internal class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            TaskScheduler taskScheduler = new TaskScheduler();
            Console.WriteLine("Task scheduling started");
            Console.WriteLine("Please select an option:");
            Console.WriteLine("************************");
            Console.WriteLine("\"1\" - add shell command");
            Console.WriteLine("\"0\" - finished adding commands - start task scheduler");
            Console.WriteLine("************************");
            Console.WriteLine("Your input:");
            var keyInput = Console.ReadLine();

            _createTaskBasedOnConsoleInput(taskScheduler, keyInput);

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            await taskScheduler.ExecuteScheduledTasks(cts.Token);

            Console.WriteLine("************************Enter anything to close program ******************");
            Console.ReadLine();
        }

        private static void _createTaskBasedOnConsoleInput(TaskScheduler taskScheduler, string keyInput)
        {
            while (keyInput.Equals("1"))
            {
                Console.WriteLine("************************");

                Console.Write("Type of command: [1] Periodic, [2] Delayed, [3] Scheduled: ");
                var inputType = Console.ReadLine();
                Console.WriteLine("");

                Console.Write("Your new command (0 to exit): ");
                var shellCommand = Console.ReadLine();
                if (shellCommand.Equals("0"))
                {
                    break;
                }

                var newTaskCreated = TaskFactory.CreateTaskBasedOnInput(shellCommand, inputType);
                if (newTaskCreated == null)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Invalid type of command entered, please try again :)");
                    continue;
                }
                taskScheduler.AddNewShellTask(newTaskCreated);

                Console.WriteLine("************************");
            }
        }

    }
}

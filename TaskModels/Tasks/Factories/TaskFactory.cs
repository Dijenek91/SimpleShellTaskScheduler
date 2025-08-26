using System;
using TaskModels.Tasks.Abstract;

namespace TaskModels.Tasks.Factories
{
    public static class TaskFactory
    {
        public static ShellTask CreateTaskBasedOnInput(string shellCommand, string inputType)
        {
            if (inputType.Equals("1", StringComparison.CurrentCultureIgnoreCase)) //periodic
            {
                Console.Write("Enter the period of time to execute the task in seconds: ");
                string periodString = Console.ReadLine();
                if (int.TryParse(periodString, out int seconds))
                {
                    int miliseconds = seconds * 1000;
                    Console.WriteLine($"Task will run at: {miliseconds * 1000} periodically");
                    return new PeriodicTask(shellCommand, miliseconds);
                }
                return null;

               
            }
            else if (inputType.Equals("2", StringComparison.CurrentCultureIgnoreCase)) //delayed
            {
                Console.Write("Enter the period of time to execute the task in seconds: ");
                string periodString = Console.ReadLine();
                if (int.TryParse(periodString, out int seconds))
                {
                    int miliseconds = seconds * 1000;
                    Console.WriteLine($"Task will run in: {seconds} seconds from trigering start with '0' ");
                    return new DelayedTask(shellCommand, miliseconds);
                }
                return null;
            }
            else if (inputType.Equals("3", StringComparison.CurrentCultureIgnoreCase)) //scheduled
            {
                Console.Write("Enter the time of day you want it to execute in format hh:mm: ");
                string scheduledTimeString = Console.ReadLine();
                DateTime scheduledTime = DateTime.MinValue;
                if (TimeSpan.TryParse(scheduledTimeString, out TimeSpan timeOfDay))
                {
                    DateTime scheduled = DateTime.Today.Add(timeOfDay);
                    Console.WriteLine($"Task will run at: {scheduled}");
                    return new ScheduledTask(shellCommand, scheduled);
                }
                return null;
            }
            else
            {
                return null;
            }
        }
    }
}

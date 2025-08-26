using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TaskModels.Tasks.Abstract;

namespace TaskModels.Tasks
{
    public class ScheduledTask : ShellTask
    {
        protected DateTime _scheduledTimeOfExecution;

        public ScheduledTask(string shellCommand , DateTime scheduledTimeOfExecution): base(shellCommand)
        {
            if (scheduledTimeOfExecution == null)
            {
                throw new ArgumentNullException(nameof(scheduledTimeOfExecution));
            }
            if(scheduledTimeOfExecution < DateTime.Now) 
            { 
                throw new ArgumentException(nameof(scheduledTimeOfExecution)+" cannot be in the past");
            }

            _scheduledTimeOfExecution = scheduledTimeOfExecution;
        }

        public override async Task Execute()
        {
            if (_scheduledTimeOfExecution <= DateTime.Now)
            {
                return; //do not execute the command
            }
            

            Console.WriteLine($"[3]Scheduled Task [{_scheduledTimeOfExecution}]: started executing.");
            
            await ProcessTaskStart(_scheduledTimeOfExecution);

            Console.WriteLine($"[3]Scheduled Task [{_scheduledTimeOfExecution}]: Finished executing");
        }

        protected async Task ProcessTaskStart(DateTime _scheduledTimeOfExecution)
        {
            await Task.Delay(_scheduledTimeOfExecution - DateTime.Now);

            //Add-Content -Path 'C:\\Temp\\output.txt' -Value \"[ $(Get-Date -Format 'HH:mm:ss') ] Scheduled Task ran!\"
            var process = Process.Start(_processStartInfo);

            process.WaitForExit();

            var output = process.StandardOutput.ReadToEnd();
        }
    }
}

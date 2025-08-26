using System.Diagnostics;
using System;
using System.Threading.Tasks;
using TaskModels.Tasks.Abstract;
using System.Timers;

namespace TaskModels.Tasks
{
    public class PeriodicTask : ShellTask
    {
        protected Timer _timer;
        protected int _periodicTime;
        public PeriodicTask(string shellcommand, int miliseconds) : base(shellcommand) 
        {
            _periodicTime = miliseconds;
            
            _timer = new Timer(_periodicTime);
            _timer.AutoReset = true;  // keep ticking
            _timer.Enabled = false; //doesnt start now
        }

        public override async Task Execute()
        {
            _timer.Elapsed += TimerExecutable;
            _timer.Enabled = true;
            _timer.Start();
        }

        public async Task StopPeriodicExecution()
        {
            _timer.Stop();

        }
        protected virtual void TimerExecutable(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine($"[1]Periodic Task [{DateTime.Now}]: executing.");

            //Add-Content -Path 'C:\\Temp\\output.txt' -Value \"[ $(Get-Date -Format 'HH:mm:ss') ] Periodic Task ran!\"
            ProceessTask();

            Console.WriteLine($"[1]Periodic Task [{DateTime.Now}]: Finished executing");
        }

        protected void ProceessTask()
        {
            var process = Process.Start(_processStartInfo);

            process.WaitForExit();

            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();

            if(!string.IsNullOrEmpty(error) && !string.IsNullOrEmpty(output)) 
            {
                Console.WriteLine($"[1]Output: {output}");
                Console.WriteLine($"[1]Error: {error}");
            }
        }
    }
}

using System;
using System.Threading.Tasks;
using System.Timers;

namespace TaskModels.Tasks
{
    public class DelayedTask : PeriodicTask
    {
        public DelayedTask(string shellCommand, int miliseconds) : base(shellCommand, miliseconds)
        {
            _timer.AutoReset = false;  // run only once ticking
            _timer.Enabled = false; //doesnt start now
        }

        public override async Task Execute()
        {
            _timer.Elapsed += TimerExecutable;
            _timer.Enabled = true;
            _timer.Start();
        }    
        
        protected override void TimerExecutable(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine($"[2]Delayed Task [{DateTime.Now}]: executing in {DateTime.Now}");

            //Add-Content -Path 'C:\\Temp\\output.txt' -Value \"[ $(Get-Date -Format 'HH:mm:ss') ] Delayed Task ran!\"
            ProceessTask();
            Console.WriteLine($"[2]Delayed Task [{DateTime.Now}]: Finished executing");
            _timer.Stop();
        }
    }
}

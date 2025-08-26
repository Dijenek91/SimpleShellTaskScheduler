using System.Diagnostics;
using System.Threading.Tasks;

namespace TaskModels.Tasks.Abstract
{
    public abstract class ShellTask : IShellCommand
    {
        private string _shellCommand;

        public abstract Task Execute();

        protected ProcessStartInfo _processStartInfo;

        public ShellTask(string shellCommand)
        {
            _shellCommand = $"-Command \"{shellCommand}\"";
            _processStartInfo = new ProcessStartInfo();
            _processStartInfo.FileName = "powershell.exe";
            _processStartInfo.Arguments = shellCommand;
            _processStartInfo.RedirectStandardOutput = true;
            _processStartInfo.RedirectStandardError = true;
            _processStartInfo.UseShellExecute = false;
            _processStartInfo.CreateNoWindow = true;
        }

    }
}

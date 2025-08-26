using System.Threading.Tasks;

namespace TaskModels.Tasks.Abstract
{
    public interface IShellCommand
    {
        Task Execute();
    }
}

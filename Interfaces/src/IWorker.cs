using System.Threading.Tasks;
using Orleans;

namespace Interfaces
{
    public interface IWorker : IGrainWithIntegerKey
    {
        Task<FanoutTaskStatus> ParentTask(
            int numberOfChildTasks,
            int taskDelayMs,
            GrainCancellationToken cancellationToken);
    }
}

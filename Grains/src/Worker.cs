using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Interfaces;
using Orleans;

namespace Grains
{
    public class Worker : Orleans.Grain, IWorker
    {
        public async Task<FanoutTaskStatus> ParentTask(
            int numberOfChildTasks,
            int taskDelayMs,
            GrainCancellationToken cancellationToken)
        {
            var tasks = new List<Task>();

            for (byte i = 0; i < numberOfChildTasks; i++)
            {
                tasks.Add(ChildTask(
                    taskDelayMs,
                    cancellationToken.CancellationToken));
            }

            await Task.WhenAll(tasks);

            int successfulTasks = tasks.Count(x => x.IsCompletedSuccessfully);

            return new FanoutTaskStatus(
                successfulTasks,
                numberOfChildTasks - successfulTasks);
        }
        
        private Task ChildTask(
            int childTaskDelayMs,
            CancellationToken cancellationToken)
        {
            return Task.Delay(
                childTaskDelayMs,
                cancellationToken);
        }
    }
}
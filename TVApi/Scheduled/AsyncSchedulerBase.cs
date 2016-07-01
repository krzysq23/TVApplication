using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Quartz;

namespace TVApi.Scheduled
{
    public abstract class AsyncSchedulerBase : IAsyncScheduler
    {
        public abstract Task ExecuteAsync(IJobExecutionContext context);

        public async void Execute(IJobExecutionContext context)
        {
            await ExecuteAsync(context).ConfigureAwait(false);
        }
        
    }
}
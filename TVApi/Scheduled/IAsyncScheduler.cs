using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TVApi.Scheduled
{
    public interface IAsyncScheduler : IJob
    {
        Task ExecuteAsync(IJobExecutionContext context);
    }
}
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TVApi.Scheduled
{
    public class AsyncScheduler : AsyncSchedulerBase
    {
        private readonly Func<Task> _command;
        public AsyncScheduler(Func<Task> command)
        {
            _command = command;
        }

        public override Task ExecuteAsync(IJobExecutionContext context)
        {
            return _command();
        }
    }
}
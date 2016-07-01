using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace TVApi.Utils
{
    public sealed class TVApiSynchContext : SynchronizationContext 
    {
        private readonly BlockingCollection<KeyValuePair<SendOrPostCallback, object>> _queue =
            new BlockingCollection<KeyValuePair<SendOrPostCallback, object>>();

        public override void Post(SendOrPostCallback d, object state)
        {
            _queue.Add(new KeyValuePair<SendOrPostCallback, object>(d, state));
        }

        public void RunOnCurrentThread()
        {
            KeyValuePair<SendOrPostCallback, object> workItem;

            while (_queue.TryTake(out workItem, Timeout.Infinite))
            {
                workItem.Key(workItem.Value);
            }

        }

        public void Complete()
        {
            _queue.CompleteAdding();
        }

        public static void Run(Func<Task> asynchFunc)
        {
            var originalContext = SynchronizationContext.Current;

            try
            {
                var synchCtx = new TVApiSynchContext();
                SynchronizationContext.SetSynchronizationContext(synchCtx);

                var task = asynchFunc();
                task.ContinueWith(delegate { synchCtx.Complete(); }, TaskScheduler.Default);

                synchCtx.RunOnCurrentThread();

                task.GetAwaiter().GetResult();
            }
            finally
            {
                SynchronizationContext.SetSynchronizationContext(originalContext);
            }
        }
    }
}
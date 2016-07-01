using BusinessLogic.Infrastructure;
using DataLayer.Entities;
using Microsoft.AspNet.Identity;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TVApi.SignalR;
using TVApi.Utils;
using TVProvider.Provider;

namespace TVApi.Scheduled
{
    public class JobScheduler
    {
        public static DateTime? date;

        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
            
            IJobDetail job = JobBuilder.Create<CommentsJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("CommentTrigger")
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(2).RepeatForever())
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }      

        public class CommentsJob : IAsyncScheduler
        {
            public void Execute(IJobExecutionContext context)
            {
                //AsyncScheduler _asyncScheduler = new AsyncScheduler(async () => await ExecuteAsync(context));
                //await _asyncScheduler.ExecuteAsync(context);

                TVApiSynchContext.Run(async delegate
                {
                    await ExecuteAsync(context);
                });
            }

            public async Task ExecuteAsync(IJobExecutionContext context)
            {
                if (date == null) { date = DateTime.Now; }
                
                EventNotifierLogic _eventNotifierLogic = new EventNotifierLogic();
                var notifications = await _eventNotifierLogic.GetCommentNotification(date.Value);
                date = DateTime.Now;
                foreach (var notif in notifications)
                {
                    ObservableNotifier.Instance.SendCommentNotificationToUser(notif);
                }
            }

        }

    }
}
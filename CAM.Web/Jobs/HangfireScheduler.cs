using System;
using Hangfire;

namespace CAM.Web.Jobs
{
    public class HangfireScheduler
    {
        public static void ScheduleRecurringJobs()
        {
            // TimesScraperJob - set for hourly
            RecurringJob.RemoveIfExists(nameof(TimesScraperJob));
            RecurringJob.AddOrUpdate<TimesScraperJob>(nameof(TimesScraperJob),
            job => job.Run(JobCancellationToken.Null),
            Cron.Hourly, TimeZoneInfo.Local);
        }
    }
}
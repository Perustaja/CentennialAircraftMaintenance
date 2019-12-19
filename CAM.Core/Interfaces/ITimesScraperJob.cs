using System.Collections.Generic;
using System.Threading.Tasks;
using CAM.Core.Entities;
using Hangfire;

namespace CAM.Core.Interfaces
{
    public interface ITimesScraperJob
    {
         Task Run(IJobCancellationToken token);
         Task UpdateTimes(ISet<Times> times);
    }
}
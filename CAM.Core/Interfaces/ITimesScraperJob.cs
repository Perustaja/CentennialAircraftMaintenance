using System.Threading.Tasks;
using Hangfire;

namespace CAM.Core.Interfaces
{
    public interface ITimesScraperJob
    {
         Task Run(IJobCancellationToken token);
         Task UpdateTimes();
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.Logging;
using CAM.Core.Interfaces;
using CAM.Infrastructure.Data;
using CAM.Web.Jobs.TimeScraper;

namespace CAM.Web.Jobs
{
    /// <summary>
    /// Implementation of ITimesScraperJob
    /// </summary>
    public class TimesScraperJob : ITimesScraperJob
    {
        private readonly ILogger<ITimesScraperJob> _logger;
        private readonly ApplicationContext _context;
        public TimesScraperJob(ApplicationContext context, ILogger<TimesScraperJob> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task Run(IJobCancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            await UpdateTimes();
        }
        public async Task UpdateTimes()
        {
            _logger.LogInformation($"{DateTime.Now}: Attempting to start scraping background service.");
            try
            {
                var scraper = new FspTimesScraper();
                var times = scraper.Run();
                foreach (var set in times)
                {
                    if (_context.Times.Any(e => e.AircraftId == set.AircraftId))
                        _context.Update(set);
                    else
                        _context.Add(set);

                    await _context.SaveChangesAsync();
                }
            }
            catch
            {
                _logger.LogError($"{DateTime.Now}: Error running scraper or saving to database. Silently failing.", null);
            }

            _logger.LogInformation($"{DateTime.Now}: Scraping service finished.");
        }
    }
}
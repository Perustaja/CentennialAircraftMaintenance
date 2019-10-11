using System;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.Logging;
using CAM.Core.Interfaces;
using CAM.Infrastructure.Data;
using CAM.Infrastructure.Jobs.TimesScraper;

namespace CAM.Infrastructure.Jobs
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
                }
            }
            catch
            {
                _logger.LogError($"{DateTime.Now}: Error running scraper. Silently failing.", null);
            }
            await _context.SaveChangesAsync();

            _logger.LogInformation($"{DateTime.Now}: Scraping service finished.");
        }
    }
}
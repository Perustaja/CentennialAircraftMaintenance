using System;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.Logging;
using CAM.Core.Interfaces;
using CAM.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace CAM.Web.Jobs
{
    /// <summary>
    /// This job runs the associated scraping job and then saves the parsed results to the Times table.
    /// </summary>
    public class TimesScraperJob : ITimesScraperJob
    {
        private readonly ApplicationContext _context;
        private readonly ITimesScraper _scraper;
        private readonly ILogger _logger;
        public TimesScraperJob(ApplicationContext context, ITimesScraper scraper, ILogger<TimesScraperJob> logger)
        {
            _context = context;
            _scraper = scraper;
            _logger = logger;
        }
        public async Task Run(IJobCancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            await UpdateTimes();
        }
        public async Task UpdateTimes()
        {
            try
            {
                _logger.LogInformation($"{DateTime.Now}: Starting times scraping job.");
                var times = _scraper.Run();
                foreach (var set in times)
                {
                    if (_context.Times.Any(e => e.AircraftId == set.AircraftId))
                        _context.Update(set);
                    else
                        _context.Add(set);
                    _logger.LogInformation($"{DateTime.Now}: Scraping job finished, Attempting to save changes.");
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"{DateTime.Now}: Changes successfully saved.");
                }
            }
            catch
            {
                _logger.LogError($"{DateTime.Now}: Scraping job experienced an error and has silently failed.", _scraper);
            }

        }
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.Logging;
using CAM.Core.Interfaces;
using CAM.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using CAM.Core.Options;
using Microsoft.Extensions.Options;

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
        public FspScraperOptions Options { get; }
        public TimesScraperJob(ApplicationContext context, ITimesScraper scraper, 
            ILogger<TimesScraperJob> logger, IOptions<FspScraperOptions> optionsAccessor)
        {
            _context = context;
            _scraper = scraper;
            _logger = logger;
            Options = optionsAccessor.Value;
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
                var times = _scraper.Run(Options);
                foreach (var set in times)
                {
                    _logger.LogInformation($"{DateTime.Now}: Scraping job finished, Attempting to save changes.");

                    if (_context.Times.Any(e => e.AircraftId == set.AircraftId))
                        _context.Update(set);
                    else
                        _context.Add(set);
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
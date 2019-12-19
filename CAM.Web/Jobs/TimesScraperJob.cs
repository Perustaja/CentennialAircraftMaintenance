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
using System.Collections.Generic;
using CAM.Core.Entities;
using OpenQA.Selenium;

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
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                stopwatch.Start();
                _logger.LogInformation($"{DateTime.Now}: Starting times scraping job.");
                var times = _scraper.Run(Options);
                _logger.LogInformation($"{DateTime.Now}: Scraping job finished in {stopwatch.Elapsed} seconds, Attempting to save changes.");
                await UpdateTimes(times);
            }
            catch (WebDriverException e)
            {
                Console.WriteLine($"{DateTime.Now}: WebDriver unable to start. Please ensure Chromium version 59 or greater is installed and is in your PATH environment variable. {e.Message}");
            }
            catch (Exception e)
            {
                _logger.LogError($"{DateTime.Now}: Scraping job experienced an error or a cancellation request and has terminated. {e}");
            }

        }
        public async Task UpdateTimes(ISet<Times> times)
        {
            try
            {
                foreach (var set in times)
                {
                    if (_context.Times.Any(e => e.AircraftId == set.AircraftId))
                        _context.Update(set);
                    else
                        _context.Add(set);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation($"{DateTime.Now}: Database changes successfully saved.");
                }
            }
            catch
            {
                _logger.LogError($"{DateTime.Now}: Scraper failed to save changes to database.");
            }

        }
    }
}
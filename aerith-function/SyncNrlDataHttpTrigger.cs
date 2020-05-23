using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Aerith.Scraper.Interfaces;
using Aerith.Data;
using Aerith.Data.Interfaces;
using Aerith.Common.Models;
using Aerith.Data.Services;
using Microsoft.EntityFrameworkCore;
using Aerith.Scraper.Services;

namespace Aerith.Function
{
    public class SyncNrlDataHttpTrigger
    {
        private readonly ILogger<SyncNrlDataHttpTrigger> _logger;
        private readonly INrlScraper _nrlScraper;

        public SyncNrlDataHttpTrigger(ILogger<SyncNrlDataHttpTrigger> logger, INrlScraper nrlScraper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _nrlScraper = nrlScraper ?? throw new ArgumentNullException(nameof(nrlScraper));
        }

        [FunctionName("SyncNrlDataHttpTrigger")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            await _nrlScraper.Scrape();

            return new OkResult();
        }
    }
}

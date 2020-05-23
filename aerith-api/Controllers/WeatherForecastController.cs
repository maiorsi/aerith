using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aerith.Common.Models;
using Aerith.Data;
using Aerith.Data.Helpers;
using Aerith.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace aerith_backend.Controllers
{
    [ApiController]
    [Route("wf")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly AerithContext _context;
        private readonly IRepository<Code> _codeRepository;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(
            IRepository<Code> codeRepository,
            ILogger<WeatherForecastController> logger,
            AerithContext context)
        {
            _codeRepository = codeRepository ?? throw new ArgumentNullException(nameof(codeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }



        [HttpGet("code")]
        public async Task<ActionResult> GetCodes()
        {
            var codes = await _codeRepository.GetQueryable()
                .IncludeMultiple(_ => _.Leagues)
                .ToListAsync();

            return Ok(codes);
        }

        [HttpPost("league")]
        public async Task<ActionResult> CreateLeague()
        {
            var code = await _context.Codes.FirstOrDefaultAsync();

            if (code != null)
            {
                var league = new League
                {
                    CodeId = code.Id,
                    Name = "Telstra Premiership",
                    Value = 111
                };

                await _context.Leagues.AddAsync(league);
                await _context.SaveChangesAsync();

                return Ok(league);
            }
            else
            {
                return NotFound("No Code Found!");
            }
        }
    }
}

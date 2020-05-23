using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Aerith.Common.Models;
using Aerith.Data.Interfaces;
using Aerith.Scraper.Interfaces;
using Aerith.Scraper.Models;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoreLinq;
using Newtonsoft.Json;

namespace Aerith.Scraper.Services
{
    public class NrlScraper : INrlScraper
    {
        private const int START_SEASON = 2010; // ACTUAL START 1908;
        private const int DEFAULT_LEAGUE = 111;
        private const int RUGBY_LEAGUE_CODE = 1;
        private const int START_ROUND = 1;

        private readonly IRepository<Code> _codeRepository;
        private readonly IRepository<League> _leagueRepository;
        private readonly ILogger<NrlScraper> _logger;
        private readonly HtmlWeb _web;
        private readonly ConcurrentDictionary<string, NrlScrapeResult> _scrapeResults;

        public NrlScraper(
            IRepository<Code> codeRepository,
            IRepository<League> leagueRepository,
            ILogger<NrlScraper> logger)
        {
            _codeRepository = codeRepository ?? throw new ArgumentNullException(nameof(codeRepository));
            _leagueRepository = leagueRepository ?? throw new ArgumentNullException(nameof(leagueRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _web = new HtmlWeb();
            _scrapeResults = new ConcurrentDictionary<string, NrlScrapeResult>();
        }

        public async Task Scrape()
        {
            var round = START_ROUND;

            var parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = 2
            };

            Parallel.For(START_SEASON, DateTime.Now.Year, parallelOptions, i =>
            {
                var scrapeResult = FetchNrlData(DEFAULT_LEAGUE.ToString(), i.ToString(), round.ToString());

                _scrapeResults.TryAdd(string.Format("S{0}.R{1}", i, round), scrapeResult);

                var lastRound = scrapeResult.FilterRounds.OrderByDescending(_ => _.Value).First().Value;

                Parallel.For(2, lastRound, parallelOptions, j =>
                {
                    var roundScrapeResult = FetchNrlData(DEFAULT_LEAGUE.ToString(), i.ToString(), j.ToString());

                    _scrapeResults.TryAdd(string.Format("S{0}.R{1}", i, j), roundScrapeResult);
                });
            });

            List<NrlScrapeFilterCompetitionResult> leagues = _scrapeResults.Values.SelectMany(_ => _.FilterCompetitions).DistinctBy(_ => _.Value).ToList();

            foreach(var league in leagues)
            {
                _logger.LogTrace("Processing league: {}", league.Name);

                var l = await _leagueRepository.GetQueryable()
                    .FirstOrDefaultAsync(_ => _.Value == league.Value);

                if (l == null)
                {
                    await _leagueRepository.Add(new League
                    {
                        Name = league.Name,
                        Value = league.Value,
                        CodeId = RUGBY_LEAGUE_CODE
                    });

                    _logger.LogInformation("Added league with name: {}", league.Name);
                }
                else
                {
                    _logger.LogInformation("Found league {} with value: {}", l.Name, l.Value);
                }
            }
        }
        public Task Scrape(League league, Season season, Round round)
        {
            throw new System.NotImplementedException();
        }

        private NrlScrapeResult FetchNrlData(string league, string season, string round)
        {
            var html = $"https://www.nrl.com/draw/?competition={league}&season={season}&round={round}";

            var htmlDoc = _web.Load(html);

            var drawNode = htmlDoc.DocumentNode
                .SelectSingleNode("//*[@id=\"vue-draw\"]");

            var qData = drawNode
                .Attributes["q-data"].Value;

            var json = HttpUtility.HtmlDecode(qData);

            var jss = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Local,
                DateParseHandling = DateParseHandling.DateTimeOffset
            };

            return JsonConvert.DeserializeObject<NrlScrapeResult>(json, jss);
        }
    }
}
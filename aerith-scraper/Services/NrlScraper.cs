using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Aerith.Common.Models;
using Aerith.Data.Interfaces;
using Aerith.Scraper.Interfaces;
using Aerith.Scraper.Models;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MoreLinq;
using Newtonsoft.Json;

namespace Aerith.Scraper.Services
{
    public class NrlScraper : INrlScraper
    {
        private const int START_SEASON = 2020; // ACTUAL START 1908;
        private const int DEFAULT_LEAGUE = 111;
        private const int RUGBY_LEAGUE_CODE = 1;
        private const int START_ROUND = 1;

        private readonly IRepository<Code> _codeRepository;
        private readonly IRepository<Fixture> _fixtureRepository;
        private readonly IRepository<League> _leagueRepository;
        private readonly IRepository<Round> _roundRepository;
        private readonly IRepository<Season> _seasonRepository;
        private readonly IRepository<Team> _teamRepository;
        private readonly IRepository<Tournament> _tournamentRepository;
        private readonly ILogger<NrlScraper> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly HtmlWeb _web;

        public NrlScraper(
            IRepository<Code> codeRepository,
            IRepository<Fixture> fixtureRepository,
            IRepository<League> leagueRepository,
            IRepository<Round> roundRepository,
            IRepository<Season> seasonRepository,
            IRepository<Team> teamRepository,
            IRepository<Tournament> tournameRepository,
            ILogger<NrlScraper> logger,
            IMemoryCache memoreCache)
        {
            _codeRepository = codeRepository ?? throw new ArgumentNullException(nameof(codeRepository));
            _fixtureRepository = fixtureRepository ?? throw new ArgumentNullException(nameof(fixtureRepository));
            _leagueRepository = leagueRepository ?? throw new ArgumentNullException(nameof(leagueRepository));
            _roundRepository = roundRepository ?? throw new ArgumentNullException(nameof(roundRepository));
            _seasonRepository = seasonRepository ?? throw new ArgumentNullException(nameof(seasonRepository));
            _teamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));
            _tournamentRepository = tournameRepository ?? throw new ArgumentNullException(nameof(tournameRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _memoryCache = memoreCache ?? throw new ArgumentNullException(nameof(memoreCache));

            _web = new HtmlWeb();
        }

        public async Task Scrape()
        {
            var round = START_ROUND;

            var scrapeResults = new ConcurrentDictionary<string, NrlScrapeResult>();

            var parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = 2
            };

            Parallel.For(START_SEASON, DateTime.Now.Year + 1, parallelOptions, i =>
            {
                var scrapeResult = FetchNrlData(DEFAULT_LEAGUE.ToString(), i.ToString(), round.ToString());

                scrapeResults.TryAdd(string.Format("S{0}.R{1}", i, round), scrapeResult);

                var lastRound = scrapeResult.FilterRounds.OrderByDescending(_ => _.Value).First().Value;

                Parallel.For(2, lastRound, parallelOptions, j =>
                {
                    var roundScrapeResult = FetchNrlData(DEFAULT_LEAGUE.ToString(), i.ToString(), j.ToString());

                    scrapeResults.TryAdd(string.Format("S{0}.R{1}", i, j), roundScrapeResult);
                });
            });

            List<NrlScrapeFilterCompetitionResult> leagues = scrapeResults.Values.SelectMany(_ => _.FilterCompetitions).DistinctBy(_ => _.Value).ToList();

            foreach(var league in leagues)
            {
                _logger.LogTrace("Processing league: {}", league.Name);

                var lg = await _leagueRepository.GetQueryable()
                    .FirstOrDefaultAsync(_ => _.Value == league.Value);

                if (lg == null)
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
                    _logger.LogInformation("Found league {} with value: {}", lg.Name, lg.Value);

                    if(!lg.Name.Equals(league.Name))
                    {
                        lg.Name = league.Name;

                        await _leagueRepository.Update(lg);
                    }
                }
            }

            List<NrlScrapeFilterSeasonResult> seasons = scrapeResults.Values.SelectMany(_ => _.FilterSeasons).DistinctBy(_ => _.Value).OrderBy(_ => _.Value).ToList();

            foreach(var season in seasons)
            {
                _logger.LogTrace("Processing season: {}", season.Name);

                var s = await _seasonRepository.GetQueryable().FirstOrDefaultAsync(_ => _.Value == season.Value);

                if (s == null)
                {
                    await _seasonRepository.Add(new Season {
                        Name = season.Name,
                        Value = season.Value
                    });

                    _logger.LogInformation("Added season with name: {}", season.Name);
                }
                else
                {
                    _logger.LogInformation("Found season {} with sesason id: {}", s.Name, s.Value);

                    if(!s.Name.Equals(season.Name))
                    {
                        s.Name = season.Name;

                        await _seasonRepository.Update(s);
                    }
                }
            }

            List<NrlScrapeFilterTeamResult> teams = scrapeResults.Values.SelectMany(_ => _.FilterTeams).DistinctBy(_ => _.Value).OrderBy(_ => _.Value).ToList();

            foreach(var team in teams)
            {
                _logger.LogTrace("Processing team: {}", team.Name);

                var t = await _teamRepository.GetQueryable().FirstOrDefaultAsync(_ => _.Value == team.Value);

                if (t == null)
                {
                    var svg = FetchSvg(team.Name);

                    await _teamRepository.Add(new Team
                    {
                        Name = team.Name,
                        Value = team.Value,
                        BadgeSVG = svg
                    });

                    _logger.LogInformation("Added team with name: {}", team.Name);
                }
                else
                {
                    _logger.LogInformation("Found team {} with team id: {}", t.Name, t.Value);

                    if(!t.Name.Equals(team.Name))
                    {
                        t.Name = team.Name;

                        await _teamRepository.Update(t);
                    }
                }
            }

            var l = await _leagueRepository.GetQueryable()
                .FirstOrDefaultAsync(_ => _.CodeId == RUGBY_LEAGUE_CODE);

            if (l == null)
            {
                _logger.LogError("Could not find league with value {0}", RUGBY_LEAGUE_CODE);
                throw new Exception("Could not find league!");
            }

            for (var i = START_SEASON; i <= DateTime.Now.Year; i++)
            {
                _logger.LogTrace("Processing tournament: League: {}, Season: {}", DEFAULT_LEAGUE, i);

                var s = await _seasonRepository.GetQueryable()
                    .FirstOrDefaultAsync(_ => _.Value == i);

                if (s == null)
                {
                    _logger.LogError("Could not find season with value {0}", i);
                    throw new Exception("Could not find season!");
                }

                
                var t = await _tournamentRepository.GetQueryable()
                    .FirstOrDefaultAsync(_ => _.LeagueId == l.Id && _.SeasonId == s.Id);

                if (t == null)
                {
                    await _tournamentRepository.Add(new Tournament
                    {
                        LeagueId = l.Id,
                        SeasonId = s.Id
                    });

                    _logger.LogInformation("Added tournament: League: {}, Season: {}", DEFAULT_LEAGUE, i);
                }
                else
                {
                    _logger.LogInformation("Found tournament League: {}, Season: {} with id: {}", t.LeagueId, i, t.Id);
                }
            }

            await SyncDatabase(scrapeResults.ToDictionary());
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

        private byte[] FetchSvg(string team)
        {
            var url = $"https://www.nrl.com/client/dist/logos/{team.ToLower()}-badge.svg";

            using(var web = new WebClient())
            {
                byte[] svg = web.DownloadData(new Uri(url));

                return svg;
            }
        }

        private async Task SyncDatabase(Dictionary<string, NrlScrapeResult> scrapeResults)
        {
            var regex = new Regex(@"S(?<season>\d+)\.R(?<round>\d+)", RegexOptions.Compiled);

            var l = await _leagueRepository.GetQueryable()
                .FirstOrDefaultAsync(_ => _.CodeId == RUGBY_LEAGUE_CODE);

            if (l == null)
            {
                _logger.LogError("Could not find league with value {0}", RUGBY_LEAGUE_CODE);
                throw new Exception("Could not find league!");
            }

            foreach (KeyValuePair<string, NrlScrapeResult> entry in scrapeResults)
            {
                var key = entry.Key;
                var scrapeResult = entry.Value;

                var match = regex.Match(key);

                if (match.Success)
                {
                    // ... Get group by name.
                    if (int.TryParse(match.Groups["season"].Value, out int season) &&
                        int.TryParse(match.Groups["round"].Value, out int round)
                    )
                    {
                        var s = await _memoryCache.GetOrCreateAsync<Season>($"S.{season}", async entry => {
                            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                            return await _seasonRepository
                                .GetQueryable()
                                .FirstOrDefaultAsync(_ => _.Value == season);
                        });

                        if (s == null)
                        {
                            _logger.LogError("Could not find season with value {0}", season);
                            throw new Exception("Could not find season!");
                        }

                        var t = await _memoryCache.GetOrCreateAsync($"T.L{l.Id}.S{s.Id}", async entry => {
                            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                            return await _tournamentRepository.GetQueryable()
                                .FirstOrDefaultAsync(_ => _.LeagueId == l.Id && _.SeasonId == s.Id);
                        });

                        if (t == null)
                        {
                            _logger.LogError("Could not find tournament with league {0} and season {1}", DEFAULT_LEAGUE, s.Id);
                            throw new Exception("Could not find tournament!");
                        }

                        var r = await _roundRepository.GetQueryable()
                            .FirstOrDefaultAsync(_ => _.TournamentId == t.Id && _.Value == round);

                        if (r == null)
                        {
                            r = await _roundRepository.Add(new Round
                            {
                                TournamentId = t.Id,
                                Value = round,
                                Name = scrapeResult.FilterRounds.First(_ => _.Value == round).Name
                            });

                            _logger.LogInformation("Added round: Tournament: {}, Round: {}", t.Id, round);
                        }
                        else
                        {
                            _logger.LogInformation("Found round: Tournament: {}, Round: {}", t.Id, round);
                        }

                        foreach(var fixture in scrapeResult.Fixtures)
                        {
                            var awayTeam = await _memoryCache.GetOrCreateAsync($"TM.{fixture.AwayTeam.TeamId}", async entry => {
                                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                                return await _teamRepository.GetQueryable()
                                    .FirstOrDefaultAsync(_ => _.Value == fixture.AwayTeam.TeamId);
                            });

                            if (awayTeam == null)
                            {
                                _logger.LogError("Could not find team with value {0}", fixture.AwayTeam.TeamId);
                                throw new Exception("Could not find team!");
                            }

                            var homeTeam = await _memoryCache.GetOrCreateAsync($"TM.{fixture.HomeTeam.TeamId}", async entry => {
                                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                                return await _teamRepository.GetQueryable()
                                    .FirstOrDefaultAsync(_ => _.Value == fixture.HomeTeam.TeamId);
                            });

                            if (homeTeam == null)
                            {
                                _logger.LogError("Could not find team with value {0}", fixture.AwayTeam.TeamId);
                                throw new Exception("Could not find team!");
                            }

                            var f = await _fixtureRepository.GetQueryable()
                                .FirstOrDefaultAsync(_ => _.RoundId == r.Id && _.AwayTeamId == awayTeam.Id && _.HomeTeamId == homeTeam.Id);

                            if (f == null)
                            {
                               await _fixtureRepository.Add(new Fixture
                                {
                                    AwayTeamId = awayTeam.Id,
                                    AwayTeamScore = fixture.AwayTeam.Score,
                                    GameMinutes = fixture.Clock.GameMinutes,
                                    HomeTeamId = homeTeam.Id,
                                    HomeTeamScore = fixture.HomeTeam.Score,
                                    KickoffTime = fixture.Clock.KickOffTimeLong,
                                    RoundId = r.Id,
                                    URL = fixture.MatchCentreUrl,
                                    MatchState = fixture.MatchState,
                                    Venue = fixture.Venue
                                });

                                _logger.LogInformation("Added fixture:  Round: {0}, Away Team: {1}, Home Team: {2}", round, awayTeam.Name, homeTeam.Name);
                            }
                            else
                            {
                                _logger.LogInformation("Found fixture: Round: {0}, Away Team: {1}, Home Team: {2}", round, awayTeam.Name, homeTeam.Name);
                            }
                        }
                    }
                }   
            }
        }
    }
}
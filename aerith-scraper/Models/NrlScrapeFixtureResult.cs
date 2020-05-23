namespace Aerith.Scraper.Models
{
    public class NrlScrapeFixtureResult
    {
        public string MatchState { get; set; }
        public string Venue { get; set; }
        public string MatchCentreUrl { get; set; }
        public NrlScrapeTeam HomeTeam { get; set; }
        public NrlScrapeTeam AwayTeam { get; set; }
        public NrlScrapeClock Clock { get; set; }
    }
}
using System.Collections.Generic;

namespace Aerith.Scraper.Models
{
    public class NrlScrapeResult
    {
        public int SelectedCompetitionId { get; set; }
        public int SelectedSeasonId { get; set; }
        public int SelectedRoundId { get; set; }
        public int SelectedTeamId { get; set; }

        public List<NrlScrapeFixtureResult> Fixtures { get; set; }
        public List<NrlScrapeFilterCompetitionResult> FilterCompetitions { get; set; }
        public List<NrlScrapeFilterSeasonResult> FilterSeasons { get; set; }
        public List<NrlScrapeFilterRoundResult> FilterRounds { get; set; }
        public List<NrlScrapeFilterTeamResult> FilterTeams { get; set; }
    }
}
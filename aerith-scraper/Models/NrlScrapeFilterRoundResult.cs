namespace Aerith.Scraper.Models
{
    public class NrlScrapeFilterRoundResult : NrlScrapeFilterResult
    {
        public virtual bool IsFinal { get { return Name.ToLower().Contains("final"); } }
    }
}
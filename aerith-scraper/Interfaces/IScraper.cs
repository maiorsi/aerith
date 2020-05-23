using System.Threading.Tasks;
using Aerith.Common.Models;

namespace Aerith.Scraper.Interfaces
{
    public interface IScraper
    {
        Task Scrape();
        Task Scrape(League league, Season season, Round round);
    }
}
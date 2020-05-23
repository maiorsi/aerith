using System.Threading.Tasks;
using Aerith.Common.Models;

namespace Aerith.Scraper.Intefaces
{
    public interface IScraper
    {
        void Scrape();
        void Scrape(League league, Season season, Round round);
    }
}
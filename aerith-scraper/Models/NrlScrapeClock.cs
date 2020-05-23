using System;

namespace Aerith.Scraper.Models
{
    public class NrlScrapeClock
    {
        public DateTime KickOffTimeLong { get; set; }

        public string GameTime { get; set; }

        public int GameMinutes { get { return int.Parse(GameTime.Substring(0,2)); } }
    }
}
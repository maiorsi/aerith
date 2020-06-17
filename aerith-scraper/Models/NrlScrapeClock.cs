using System;

namespace Aerith.Scraper.Models
{
    public class NrlScrapeClock
    {
        public DateTime KickOffTimeLong { get; set; }

        public string GameTime { get; set; }

        public int GameMinutes { 
            get { 
                return ParseGameMinutes();
            } 
        }

        private int ParseGameMinutes()
        {
            int.TryParse(GameTime.Substring(0, GameTime.IndexOf(":")), out int gameMinutes);

            return gameMinutes;
        }
    }
}
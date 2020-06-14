using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aerith.Common.Models
{
    [Table("fixtures")]
    public class Fixture : MetaDbType
    {
        // Properties
        [Column("roundId")]
        [Index]
        public long RoundId { get; set; }

        [MaxLength(64)]
        [Column("matchState")]
        public string MatchState { get; set; }

        [MaxLength(256)]
        [Column("venue")]
        public string Venue { get; set; }

        [MaxLength(1024)]
        [Column("url")]
        public string URL { get; set; }

        [Column("homeTeamId")]
        [Index]
        public long HomeTeamId { get; set; }

        [Column("awayTeamId")]
        [Index]
        public long AwayTeamId { get; set; }

        [Column("teamId")]
        public long? TeamId { get; set; }

        [Column("homeTeamScore")]
        public int HomeTeamScore { get; set; }

        [Column("awayTeamScore")]
        public int AwayTeamScore { get; set; }

        [Column("kickoffTime")]
        public DateTime KickoffTime { get; set; }

        [Column("gameMinutes")]
        public int GameMinutes { get; set; }

        // Navigation Properties
        [Required]
        [ForeignKey("RoundId")]
        public virtual Round Round { get; set; }

        [Required]
        [ForeignKey("HomeTeamId")]
        public virtual Team HomeTeam { get; set; }

        [Required]
        [ForeignKey("AwayTeamId")]
        public virtual Team AwayTeam { get; set; }

        [InverseProperty("Fixture")]
        public virtual List<Tip> Tips { get; set; }
    }
}
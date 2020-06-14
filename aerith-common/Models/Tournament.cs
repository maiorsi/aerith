using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aerith.Common.Models
{
    [Table("tournaments")]
    public class Tournament : MetaDbType
    {
        public Tournament()
        {
            this.Competitions = new List<Competition>();
            this.Rounds = new List<Round>();
        }
        // Properties
        [Column("leagueId")]
        public long LeagueId { get; set; }

        [Column("seasonId")]
        public long SeasonId { get; set; }

        // Navigation Properties
        [Required]
        [ForeignKey("LeagueId")]
        public virtual League League { get; set; }

        [Required]
        [ForeignKey("SeasonId")]
        public virtual Season Season { get; set; }

        [InverseProperty("Tournament")]
        public virtual List<Round> Rounds { get; set;}

        [InverseProperty("Tournament")]
        public virtual List<Competition> Competitions { get; set;}
    }
}
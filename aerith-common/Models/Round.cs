using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aerith.Common.Models
{
    [Table("rounds")]
    public class Round : MetaDbType
    {
        public Round()
        {
            this.Fixtures = new List<Fixture>();
        }
        
        // Properties
        [Column("name")]
        public string Name { get; set; }

        [Column("value")]
        public int Value { get; set; }

        [Column("tournamentId")]
        public long TournamentId { get; set; }

        // Navigation Properties
        [Required]
        [ForeignKey("TournamentId")]
        public virtual Tournament Tournament { get; set; }

        [InverseProperty("Round")]
        public virtual List<Fixture> Fixtures { get; set; }
    }
}
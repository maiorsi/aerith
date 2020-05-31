using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Aerith.Common.Models
{
    [Table("teams")]
    public class Team : MetaDbType
    {
        public Team()
        {
            this.AwayFixtures = new List<Fixture>();
            this.Byes = new List<Bye>();
            this.HomeFixtures = new List<Fixture>();
        }
        // Properties
        [Column("value")]
        [Index(IsUnique = true)]
        public int Value { get; set; }

        [MaxLength(128)]
        [Column("name")]
        public string Name { get; set; }

        [Column("badgeSVG")]
        public byte[] BadgeSVG { get; set; }

        // Navigation Properties
        [InverseProperty("HomeTeam")]
        public virtual List<Fixture> HomeFixtures { get; set; }

        [InverseProperty("AwayTeam")]
        public virtual List<Fixture> AwayFixtures { get; set; }

        [InverseProperty("Team")]
        public virtual List<Bye> Byes { get; set; }

        public virtual List<Fixture> Fixtures { get { return HomeFixtures.Union(AwayFixtures).ToList(); } }

        [InverseProperty("Team")]
        public virtual List<Tip> Tips { get; set; }
    }
}
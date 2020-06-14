using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aerith.Common.Models
{
    [Table("tips")]
    public class Tip : MetaDbType
    {
        // Properties
        [Required]
        [Index]
        [Column("userId")]
        public long UserId { get; set; }

        [Required]
        [Column("fixtureId")]
        public long FixtureId { get; set; }

        [Required]
        [Column("competitionId")]
        public long CompetitionId { get; set; }

        [Required]
        [Column("selectedTeamId")]
        public long? SelectedTeamId { get; set; }

        // Navigation Properties
        [Required]
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        [ForeignKey("FixtureId")]
        public virtual Fixture Fixture { get; set; }

        [Required]
        [ForeignKey("CompetitionId")]
        public virtual Competition Competition { get; set; }

        [Required]
        [ForeignKey("SelectedTeamId")]
        public virtual Team Team { get; set; }
    }
}
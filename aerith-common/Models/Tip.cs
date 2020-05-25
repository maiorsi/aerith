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
        public int UserId { get; set; }

        [Required]
        [Column("fixtureId")]
        public int FixtureId { get; set; }

        [Required]
        [Column("competitionId")]
        public int CompetitionId { get; set; }

        [Required]
        [Column("selectedTeamId")]
        public int SelectedTeamId { get; set; }

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
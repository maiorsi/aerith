using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aerith.Common.Models
{
    [Table("competitions")]
    public class Competition : MetaDbType
    {
        // Properties
        [Column("groupId")]
        public long GroupId { get; set; }

        [Column("tournamentId")]
        public long TournamentId { get; set; }

        // Navigation Properties
        [Required]
        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }

        [Required]
        [ForeignKey("TournamentId")]
        public virtual Tournament Tournament { get; set; }

        [InverseProperty("Competition")]
        public virtual List<Tip> Tips { get; set; }
    }
}
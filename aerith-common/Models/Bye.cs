using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aerith.Common.Models {
    
    [Table("byes")]
    public class Bye : MetaDbType 
    {
        // Properties
        [Column("roundId")]
        public long RoundId { get; set; }

        [Column("teamId")]
        public long TeamId { get; set; }

        // Navigation Properties
        [Required]
        [ForeignKey("TeamId")]
        public virtual Team Team { get; set; }
    }
}
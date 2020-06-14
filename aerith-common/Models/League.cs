using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aerith.Common.Models
{
    [Table("leagues")]
    public class League : MetaDbType
    {
        public League()
        {
            this.Tournaments = new List<Tournament>();
        }
        
        [Required]
        [Column("codeId")]
        public long CodeId { get; set; }

        [Index]
        [Column("value")]
        [Index(IsUnique = true)]
        public int Value { get; set; }

        [MaxLength(256)]
        [Index(IsUnique = true)]
        [Column("name")]
        public string Name { get; set; }

        // Navigation Properties
        [Required]
        [ForeignKey("CodeId")]
        public virtual Code Code { get; set; }

        [InverseProperty("League")]
        public virtual List<Tournament> Tournaments { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aerith.Common.Models
{
    [Table("seasons")]
    public class Season : MetaDbType
    {
        public Season()
        {
            this.Tournaments = new List<Tournament>();
        }
        
        // Properties
        [MaxLength(64)]
        [Column("name")]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [Column("value")]
        public int Value { get; set; }

        // Navigation Properties
        [InverseProperty("Season")]
        public virtual List<Tournament> Tournaments { get; set; }
    }
}
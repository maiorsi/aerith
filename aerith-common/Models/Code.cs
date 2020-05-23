using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aerith.Common.Models
{
    [Table("codes")]
    public class Code : MetaDbType
    { 
        public Code()
        {
            this.Leagues = new List<League>();
        }

        // Properties
        [MaxLength(128)]
        [Column("name")]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        // Navigation Properties
        [InverseProperty("Code")]
        public virtual List<League> Leagues { get; set; }
    }
}
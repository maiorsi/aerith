using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aerith.Common.Models
{
    public class MetaDbType
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [MaxLength(128)]
        [Column("createdBy")]
        [DefaultValue("AERITH")]
        public string CreatedBy { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("createdDate")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [MaxLength(128)]
        [Column("modifiedBy")]
        [DefaultValue("AERITH")]
        public string ModifiedBy { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("modifiedDate")]
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        [Column("isInactive")]
        [DefaultValue(false)]
        public bool IsInactive { get; set; }
    }
}
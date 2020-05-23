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
        public int Id { get; set; }

        [MaxLength(128)]
        [Column("createdBy")]
        [DefaultValue("AERITH")]
        public string CreatedBy { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("createdDate")]
        [DefaultValue("DATETIME()")]
        public DateTime CreatedDate { get; set; }

        [MaxLength(128)]
        [Column("modifiedBy")]
        [DefaultValue("AERITH")]
        public string ModifiedBy { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("modifiedDate")]
        [DefaultValue("DATETIME()")]
        public DateTime ModifiedDate { get; set; }

        [Column("isInactive")]
        [DefaultValue(false)]
        public bool IsInactive { get; set; }
    }
}
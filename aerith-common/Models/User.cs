using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aerith.Common.Models
{
    [Table("users")]
    public class User : MetaDbType
    {
        // Properties
        [Required]
        [Index]
        [MaxLength(256)]
        [Column("loginId")]
        public string LoginId { get; set; }

        [MaxLength(256)]
        [Column("name")]
        public string Name { get; set; }

        [Column("groupId")]
        public int? GroupId { get; set; }

        // Navigation Properties
        [InverseProperty("User")]
        public virtual List<GroupUser> GroupUsers { get; set; }

        [InverseProperty("User")]
        public virtual List<Tip> Tips { get; set; }
    }
}
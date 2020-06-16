using System.ComponentModel.DataAnnotations.Schema;
using Aerith.Common.Models.Identity;

namespace Aerith.Common.Models
{
    [Table("groupUsers")]
    public class GroupUser : MetaDbType
    {
        // Properties
        [Column("userId")]
        public long UserId { get; set; }

        [Column("groupId")]
        public long GroupId { get; set; }

        // Navigation Properties
        [ForeignKey("UserId")]
        public virtual ApplicationUser User {get; set;}

        [ForeignKey("GroupId")]
        public virtual Group Group {get; set;}
    }
}
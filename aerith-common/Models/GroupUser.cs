using System.ComponentModel.DataAnnotations.Schema;

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
        public virtual User User {get; set;}

        [ForeignKey("GroupId")]
        public virtual Group Group {get; set;}
    }
}
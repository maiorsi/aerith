using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Aerith.Common.Models.Identity;

namespace Aerith.Common.Models
{
    [Table("users")]
    public class User : MetaDbType
    {
        public User()
        {
            this.GroupUsers = new List<GroupUser>();
        }

        // Properties
        [Column("identityId")]
        public long IdentityId { get; set; }

        [MaxLength(256)]
        [Column("name")]
        public string Name { get; set; }

        [Column("groupId")]
        public long? GroupId { get; set; }

        // Navigation Properties
        [ForeignKey("IdentityId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [InverseProperty("User")]
        public virtual List<GroupUser> GroupUsers { get; set; }

        [InverseProperty("User")]
        public virtual List<Tip> Tips { get; set; }

        public virtual List<Group> Groups { get { return GroupUsers.Select(_ => _.Group).ToList(); }}
    }
}
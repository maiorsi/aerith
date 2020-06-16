using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Aerith.Common.Models.Identity;

namespace Aerith.Common.Models
{
    [Table("groups")]
    public class Group : MetaDbType
    {
        public Group()
        {
            this.Competitions = new List<Competition>();
            this.GroupUsers = new List<GroupUser>();
        }

        // Properties
        [MaxLength(128)]
        [Column("name")]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        // Navigation Properties
        [InverseProperty("Group")]
        public virtual List<Competition> Competitions { get; set; }

        [InverseProperty("Group")]
        public virtual List<GroupUser> GroupUsers { get; set; }

        public virtual List<ApplicationUser> Users { get { return GroupUsers.Select(_ => _.User).ToList(); }}
    }
}
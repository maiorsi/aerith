using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace Aerith.Common.Models.Identity
{
    public class ApplicationUser : IdentityUser<long>
    {
        public ApplicationUser()
        {
            this.GroupUsers = new List<GroupUser>();
        }

        public virtual List<ApplicationUserClaim> Claims { get; set; }
        
        public virtual List<ApplicationUserLogin> Logins { get; set; }

        public virtual List<ApplicationUserToken> Tokens { get; set; }

        public virtual List<ApplicationUserRole> Roles { get; set; }

        [MaxLength(256)]
        [Column("name")]
        public string Name { get; set; }

        [MaxLength(256)]
        [Column("nickname")]
        public string Nickname { get; set; }

        [InverseProperty("User")]
        public virtual List<GroupUser> GroupUsers { get; set; }

        [InverseProperty("User")]
        public virtual List<Tip> Tips { get; set; }

        public virtual List<Group> Groups { get { return GroupUsers.Select(_ => _.Group).ToList(); }}
    }
}

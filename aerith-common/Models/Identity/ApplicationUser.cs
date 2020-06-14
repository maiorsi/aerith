using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Aerith.Common.Models.Identity
{
    public class ApplicationUser : IdentityUser<long>
    {
        [InverseProperty("ApplicationUser")]
        public virtual User User { get; set; }

        public virtual List<ApplicationUserClaim> Claims { get; set; }
        
        public virtual List<ApplicationUserLogin> Logins { get; set; }

        public virtual List<ApplicationUserToken> Tokens { get; set; }

        public virtual List<ApplicationUserRole> Roles { get; set; }
    }
}

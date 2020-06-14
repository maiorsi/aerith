using System;
using Microsoft.AspNetCore.Identity;

namespace Aerith.Common.Models.Identity
{
    public class ApplicationUserRole : IdentityUserRole<long>
    {
        public virtual ApplicationUser User { get; set; }

        public virtual ApplicationRole Role { get; set; }
    }
}
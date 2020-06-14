using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Aerith.Common.Models.Identity
{
    public class ApplicationRole : IdentityRole<long>
    {
        public virtual List<ApplicationRoleClaim> Claims { get; set; }

        public virtual List<ApplicationUserRole> Users { get; set; }
    }
}
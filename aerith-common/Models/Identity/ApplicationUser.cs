using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Aerith.Common.Models.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [InverseProperty("ApplicationUser")]
        public virtual User User { get; set; }
    }
}

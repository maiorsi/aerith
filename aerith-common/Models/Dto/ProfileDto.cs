using System.Collections.Generic;

namespace Aerith.Common.Models.Dto
{
    public class ProfileDto
    {
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }

        public List<string> Roles { get; set; }
    }
}
using Aerith.Common.Models;
using Aerith.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Aerith.Api.Controllers
{
    public class GroupUsersController : BaseController<GroupUser>
    {
        public GroupUsersController(ILogger<GroupUsersController> logger, IRepository<GroupUser> repository)
            : base(logger, repository)
        {           
        }
    }
}
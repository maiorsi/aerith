using Aerith.Common.Models;
using Aerith.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Aerith.Api.Controllers
{
    [AllowAnonymous]
    public class UsersController : BaseController<User>
    {
        public UsersController(ILogger<UsersController> logger, IRepository<User> repository)
            : base(logger, repository)
        {           
        }
    }
}
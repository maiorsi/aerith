using Aerith.Common.Models;
using Aerith.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Aerith.Api.Controllers
{
    [Authorize(Policy = "AdministratorsOnly")]
    public class TeamsController : BaseController<Team>
    {
        public TeamsController(ILogger<TeamsController> logger, IRepository<Team> repository)
            : base(logger, repository)
        {           
        }
    }
}
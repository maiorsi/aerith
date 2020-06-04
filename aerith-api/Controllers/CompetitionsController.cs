using Aerith.Common.Models;
using Aerith.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Aerith.Api.Controllers
{
    [AllowAnonymous]
    public class CompetitionsController : BaseController<Competition>
    {
        public CompetitionsController(ILogger<CompetitionsController> logger, IRepository<Competition> repository)
            : base(logger, repository)
        {           
        }
    }
}
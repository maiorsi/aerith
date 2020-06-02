using Aerith.Common.Models;
using Aerith.Data.Interfaces;
using Microsoft.Extensions.Logging;

namespace Aerith.Api.Controllers
{
    public class CompetitionsController : BaseController<Competition>
    {
        public CompetitionsController(ILogger<CompetitionsController> logger, IRepository<Competition> repository)
            : base(logger, repository)
        {           
        }
    }
}
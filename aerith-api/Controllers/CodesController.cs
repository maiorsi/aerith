using Aerith.Common.Models;
using Aerith.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Aerith.Api.Controllers
{
    [Authorize(Policy = "AdministratorsOnly")]
    public class CodesController : BaseController<Code>
    {
        public CodesController(ILogger<CodesController> logger, IRepository<Code> repository)
            : base(logger, repository)
        {           
        }
    }
}
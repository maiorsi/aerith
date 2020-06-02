using Aerith.Common.Models;
using Aerith.Data.Interfaces;
using Microsoft.Extensions.Logging;

namespace Aerith.Api.Controllers
{
    public class CodesController : BaseController<Code>
    {
        public CodesController(ILogger<CodesController> logger, IRepository<Code> repository)
            : base(logger, repository)
        {           
        }
    }
}
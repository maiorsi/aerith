using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aerith.Common.Models;
using Aerith.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Aerith.Api.Controllers
{
    [Authorize(Policy = "AdministratorsOnly")]
    public class LeaguesController : BaseController<League>
    {
        private readonly ILogger<LeaguesController> _logger;
        private readonly IRepository<League> _repository;
        public LeaguesController(ILogger<LeaguesController> logger, IRepository<League> repository)
            : base(logger, repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Gets all leagues
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        [HttpGet("all")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(typeof(IEnumerable<League>))]
        public async new Task<ActionResult<League>> Get_1_0(ApiVersion version)
        {
            _logger.LogTrace("GET api/v{}.{}/leagues/all", version.MajorVersion, version.MinorVersion);
            
            var leagues = await _repository
                    .GetQueryable()
                    .Include(_ => _.Code)
                    .ToListAsync();

            return Ok(leagues);
        }
    }
}
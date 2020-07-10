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
    public class TournamentsController : BaseController<Tournament>
    {
        private readonly ILogger<TournamentsController> _logger;
        private readonly IRepository<Tournament> _repository;
        public TournamentsController(ILogger<TournamentsController> logger, IRepository<Tournament> repository)
            : base(logger, repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Gets all tournaments
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        [HttpGet("all")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(typeof(IEnumerable<Tournament>))]
        public async new Task<ActionResult<Tournament>> Get_1_0(ApiVersion version)
        {
            _logger.LogTrace("GET api/v{}.{}/tournaments/all", version.MajorVersion, version.MinorVersion);
            
            var tournaments = await _repository
                    .GetQueryable()
                    .Include(_ => _.League)
                    .Include(_ => _.Season)
                    .Include(_ => _.Competitions)
                    .ToListAsync();

            return Ok(tournaments);
        }
    }
}
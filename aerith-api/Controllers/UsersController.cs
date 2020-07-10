using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aerith.Common.Models;
using Aerith.Common.Models.Identity;
using Aerith.Data;
using Aerith.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Aerith.Api.Controllers
{
    [Route("api/v{version:apiVersion}/users")]
    [Authorize(Policy = "AdministratorsOnly")]
    public class ApplicationUsersController : ControllerBase
    {
        private readonly ILogger<ApplicationUsersController> _logger;
        private readonly AerithContext _context;

        public ApplicationUsersController(
            ILogger<ApplicationUsersController> logger,
            AerithContext context)
            : base()
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Gets all tournaments
        /// </summary>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        [HttpGet("all")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(typeof(IEnumerable<ApplicationUser>))]
        public async Task<ActionResult<ApplicationUser>> Get_1_0(ApiVersion version)
        {
            _logger.LogTrace("GET api/v{}.{}/users/all", version.MajorVersion, version.MinorVersion);
            
            var users = await _context
                .Users
                .AsQueryable()
                .ToListAsync();

            return Ok(users);
        }
    }
}
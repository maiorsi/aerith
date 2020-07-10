using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Aerith.Common.Models;
using Aerith.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Aerith.Data.Helpers;

namespace Aerith.Api.Controllers
{
    [Authorize(Policy = "AdministratorsOnly")]
    public class GroupsController : BaseController<Group>
    {
        private readonly ILogger<GroupsController> _logger;
        private readonly IRepository<Group> _repository;
        
        public GroupsController(ILogger<GroupsController> logger, IRepository<Group> repository)
            : base(logger, repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(logger));       
        }

        /// <summary>
        /// Get an entity by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        [HttpGet("_/{id:long}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(typeof(Group))]
        public async new Task<ActionResult<Group>> GetOne_1_0([FromRoute] long id,  ApiVersion version)
        {
            _logger.LogTrace("GET api/v{}.{}/[controller]/{}", version.MajorVersion, version.MinorVersion, id);
            
            var group = await _repository
                    .GetQueryable()
                    .Include(_ => _.Users)
                    .FirstOrDefaultAsync(_ => _.Id == id);

            if(group == null)
            {
                return NotFound();
            }

            return Ok(group);
        }
    }
}
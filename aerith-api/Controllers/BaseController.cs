using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aerith.Common.Models;
using Aerith.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aerith.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseController<T> : ControllerBase where T: MetaDbType
    {
        private readonly ILogger<BaseController<T>> _logger;
        private readonly IRepository<T> _repository;

        public BaseController(ILogger<BaseController<T>> logger, IRepository<T> repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Get All Users
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<T>>> Get_1_0(ApiVersion version, [FromQuery] bool eager = false)
        {
            _logger.LogTrace("GET api/v{}.{}/[controller]", version.MajorVersion, version.MinorVersion);

            return Ok(await _repository.GetAllAsync(eager));
        }

        /// <summary>
        /// Get an entity by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        [HttpGet("{id:long}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<T>> GetOne_1_0([FromRoute] long id,  ApiVersion version, [FromQuery] bool eager = false)
        {
            _logger.LogTrace("GET api/v{}.{}/[controller]/{}", version.MajorVersion, version.MinorVersion, id);
            
            return await _repository.GetAsync(id, eager);
        }

        /// <summary>
        /// Create a new entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        [HttpPost()]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<T>> Create_1_0([FromBody] T entity, ApiVersion version)
        {
            _logger.LogTrace("POST api/v{}.{}/[controller]", version.MajorVersion, version.MinorVersion);
            
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _repository.AddAsync(entity));
        }

        /// <summary>
        /// Overwrite an entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        [HttpPut("{id:long}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<T>> Overwrite_1_0([FromRoute] long id, [FromBody] T entity, ApiVersion version)
        {
            _logger.LogTrace("PUT api/v{}.{}/[controller]/{}", version.MajorVersion, version.MinorVersion, id);
            
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _repository.UpdateAsync(entity));
        }

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jsonPatch"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        [HttpPatch("{id:long}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<T>> Update_1_0([FromRoute] long id, [FromBody] JsonPatchDocument<T> jsonPatch, ApiVersion version)
        {
            _logger.LogTrace("PATCH api/v{}.{}/[controller]/{}", version.MajorVersion, version.MinorVersion, id);
            
            var u = await _repository.GetAsync(id);

            if(u == null)
            {
                return NotFound();
            }

            jsonPatch.ApplyTo(u);

            return Ok(await _repository.UpdateAsync(u));
        }

        [HttpDelete("{id:long}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete_1_0([FromRoute] long id, ApiVersion version)
        {
            _logger.LogTrace("DELETE api/v{}.{}/[controller]/{}", version.MajorVersion, version.MinorVersion, id);
            
            var u = await _repository.GetAsync(id);

            if(u == null)
            {
                return NotFound();
            }

            await _repository.RemoveAsync(id);

            return Ok();
        }
    }
}
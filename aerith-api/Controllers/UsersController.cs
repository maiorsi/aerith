using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aerith.Common.Models;
using Aerith.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aerith.Api.Controllers
{
    /// <summary>
    /// Users Controller
    /// </summary>
    [AllowAnonymous]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/users")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IRepository<User> _userRepository;

        /// <summary>
        /// Constructor for <see cref="UsersController"/>
        /// </summary>
        /// <param name="logger"></param>
        public UsersController(ILogger<UsersController> logger, IRepository<User> userRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        /// <summary>
        /// Get All Users
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<User>>> Get_1_0(ApiVersion version)
        {
            _logger.LogTrace("GET api/v{}.{}/users", version.MajorVersion, version.MinorVersion);

            return Ok(await _userRepository.GetAll());
        }

        /// <summary>
        /// Get a user by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        [HttpGet(":id")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<User>> GetOne_1_0([FromRoute] int id, ApiVersion version)
        {
            _logger.LogTrace("GET api/v{}.{}/users/{}", version.MajorVersion, version.MinorVersion, id);
            
            return await _userRepository.Get(id);
        }

        /// <summary>
        /// Create a new User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        [HttpPost()]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<User>> Create_1_0([FromBody] User user, ApiVersion version)
        {
            _logger.LogTrace("POST api/v{}.{}/users", version.MajorVersion, version.MinorVersion);
            
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userRepository.Add(user));
        }

        /// <summary>
        /// Overwrite a User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        [HttpPut(":id")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<User>> Overwrite_1_0([FromRoute] int id, [FromBody] User user, ApiVersion version)
        {
            _logger.LogTrace("PUT api/v{}.{}/users/{}", version.MajorVersion, version.MinorVersion, id);
            
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userRepository.Update(user));
        }

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jsonPatch"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        [HttpPatch(":id")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<User>> Update_1_0([FromRoute] int id, [FromBody] JsonPatchDocument<User> jsonPatch, ApiVersion version)
        {
            _logger.LogTrace("PATCH api/v{}.{}/users/{}", version.MajorVersion, version.MinorVersion, id);
            
            var u = await _userRepository.Get(id);

            if(u == null)
            {
                return NotFound();
            }

            jsonPatch.ApplyTo(u);

            return Ok(await _userRepository.Update(u));
        }

        [HttpDelete(":id")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Delete_1_0([FromRoute] int id, ApiVersion version)
        {
            _logger.LogTrace("DELETE api/v{}.{}/users/{}", version.MajorVersion, version.MinorVersion, id);
            
            var u = await _userRepository.Get(id);

            if(u == null)
            {
                return NotFound();
            }

            await _userRepository.Remove(id);

            return Ok();
        }
    }
}

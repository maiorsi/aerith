using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aerith.Common.Models.Dto;
using Aerith.Common.Models.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aerith.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/account")]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register_1_0([FromBody] RegistrationDto registration, ApiVersion apiVersion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<ApplicationUser>(registration);

            var result = await _userManager.CreateAsync(user, registration.Password);

            if(!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpGet("profile")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces(typeof(ProfileDto))]
        public async Task<IActionResult> Profile_1_0(ApiVersion apiVersion) 
        {
            var identity = HttpContext.User.Identity;

            if(identity == null)
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByNameAsync(identity.Name);

            if(user == null)
            {
                return Unauthorized();
            }

            var roles = await _userManager.GetRolesAsync(user);

            var profile = new ProfileDto {
                Name = user.Name,
                Nickname = user.Nickname,
                Email = user.Email,
                Username = user.UserName,
                Roles = new List<string>(roles)
            };

            return Ok(profile);
        }
    }
}
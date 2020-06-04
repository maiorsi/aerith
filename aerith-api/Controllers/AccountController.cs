using System;
using System.Threading.Tasks;
using Aerith.Common.Models;
using Aerith.Common.Models.Dto;
using Aerith.Common.Models.Identity;
using Aerith.Data.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aerith.Api.Controllers
{
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/account")]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<User> _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(IMapper mapper, IRepository<User> userRepository, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [HttpPost("register")]
        [MapToApiVersion("1.0")]
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

            await _userRepository.AddAsync(new User {
                IdentityId = user.Id,
                Name = registration.Name,
                LoginId = user.UserName
            });

            return Ok();
        }
    }
}
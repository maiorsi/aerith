using Aerith.Common.Models;
using Aerith.Common.Models.Dto;
using Aerith.Common.Models.Identity;
using AutoMapper;

namespace Aerith.Api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegistrationDto, ApplicationUser>().ForMember(_ => _.UserName, map => map.MapFrom(_ => _.Email));;
        }
    }
}
using AutoMapper;
using Kupa.Api.Dtos;

namespace Kupa.Api.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserProfileDto, Kupa.Api.Models.UserProfile>();
        }
    }
}

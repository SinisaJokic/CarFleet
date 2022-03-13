using AutoMapper;

namespace CarFleetAPI.Profiles
{
    public class UserModelProfile : Profile
    {
        public UserModelProfile()
        {
            CreateMap<Entities.UserModel, Models.UserModelDto>();
            CreateMap<Models.UserModelDto, Entities.UserModel>();
        }
    }
}

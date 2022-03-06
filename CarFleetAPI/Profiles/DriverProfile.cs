using AutoMapper;

namespace CarFleetAPI.Profiles
{
    public class DriverProfile :Profile
    {
        public DriverProfile()
        {
            CreateMap<Entities.Driver, Models.DriverDto>();
            CreateMap<Models.DriverDto, Entities.Driver>();
        }
    }
}

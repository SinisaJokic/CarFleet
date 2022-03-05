using AutoMapper;

namespace CarFleetAPI.Profiles
{
    public class VehicleProfile :Profile
    {
        public VehicleProfile()
        {
            CreateMap<Entities.Vehicle, Models.VehicleDto>();
            CreateMap<Models.VehicleDto, Entities.Vehicle>();
        }
    }
}

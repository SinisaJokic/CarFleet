using AutoMapper;
using CityInfo.API.Models;

namespace CarFleetAPI.Profiles
{
    public class VehicleAssignProfile :Profile
    {
        public VehicleAssignProfile()
        {
            CreateMap<Entities.VehicleAssign, VehicleAssignDto>()
                .ForMember(dest => dest.RegistrationNumber,act => act.MapFrom(src => src.Vehicle.RegistrationNumber ))
                .ForMember(dest => dest.Driver, act => act.MapFrom(src => src.Driver.FirstName + " " + src.Driver.LastName));
            CreateMap<VehicleAssignDto, Entities.VehicleAssign>();
            CreateMap<Entities.VehicleAssign, VehicleAssignWithoutVehicleDriverDto>();
            CreateMap<VehicleAssignWithoutVehicleDriverDto, Entities.VehicleAssign>();


        }
    }
}

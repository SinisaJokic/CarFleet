using AutoMapper;
using CityInfo.API.Models;

namespace CarFleetAPI.Profiles
{
    public class VehicleAssignProfile :Profile
    {
        public VehicleAssignProfile()
        {
            CreateMap<Entities.VehicleAssign, VehicleAssignDto>();
            CreateMap<VehicleAssignDto, Entities.VehicleAssign>();
        }
    }
}

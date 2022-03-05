using CarFleetAPI.Models;

namespace CarFleetAPI
{
    public class VehicleDataStore
    {
        public List<VehicleDto> Vehicles { get; set; }
       //public static object Current { get; internal set; }

        public static VehicleDataStore Current { get; } = new VehicleDataStore();

        public VehicleDataStore()
        {
            Vehicles = new List<VehicleDto>()
            {
                new VehicleDto()
                {
                    Id = 1,
                    Model="Mercedes",
                    RegistrationNumber="ST-111-AA"
                },
                new VehicleDto()
                {
                    Id = 2,
                    Model="Volvo",
                    RegistrationNumber="ST-222-BB"
                }
            };
        }
    }
}

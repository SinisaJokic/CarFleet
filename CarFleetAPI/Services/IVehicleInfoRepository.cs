using CarFleetAPI.Entities;

namespace CarFleetAPI.Services
{
    public interface IVehicleInfoRepository
    {
        Task<IEnumerable<Vehicle>> GetVehiclesAsync();
        Task<Vehicle?> GetVehicleAsync(int vehicleId);
        Task<bool> VehicleExistsAsync(int vehicleId);
        //Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);
        //Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId,
        //    int pointOfInterestId);
        Task AddVehicleAsync(Vehicle vehicle);
        void DeleteVehicle(Vehicle vehicle);
        Task<bool> SaveChangesAsync();
    }
}

using CarFleetModels;

namespace CarFleetUI.Services
{
    public interface IVehicleService
    {
        Task<List<Vehicle>> GetAllVehicle();
        Task<Vehicle> GetVehicleDetails(int vehicleId);
        Task<Vehicle> AddVehicle(Vehicle vehicle);
        Task UpdateVehicle(Vehicle vehicle);
        Task DeleteVehicle(int vehicleId);
    }
}

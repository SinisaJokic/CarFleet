using CarFleetModels;

namespace CarFleetUI.Services
{
    public interface IVehicleService
    {
        UserModel User { get; }
        //User UserTemp { get; }
        Task Initialize();
        Task<List<Vehicle>> GetAllVehicle();
        Task<Vehicle> GetVehicleDetails(int vehicleId);
        Task<Vehicle> AddVehicle(Vehicle vehicle);
        Task UpdateVehicle(Vehicle vehicle);
        Task DeleteVehicle(int vehicleId);
    }
}

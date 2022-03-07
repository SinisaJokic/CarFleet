using CarFleetAPI.Entities;

namespace CarFleetAPI.Services
{
    public interface IVehicleAssignRepository
    {
        Task<IEnumerable<VehicleAssign>> GetVehicleAssignAsync();
        Task<VehicleAssign?> GetVehicleAssignAsync(int vehicleAssignId);
        Task<bool> VehicleAssignExistsAsync(int vehicleAssignId);
        //Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);
        //Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId,
        //    int pointOfInterestId);
        Task AddVehicleAssignAsync(VehicleAssign vehicleAssign);
        void DeleteVehicleAssign(VehicleAssign vehicleAssign);
        Task<bool> SaveChangesAsync();
        Task<bool> ExistsVehicleDriverDateAsync(int vehicleId,int driverID,DateTime dateTimeFrom, DateTime dateTimeTo);
    }
}

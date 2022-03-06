using CarFleetModels;

namespace CarFleetUI.Services
{
    public interface IAssignmentService
    {
        Task<List<VehicleAssign>> GetAllVehicleAssign();
        Task<VehicleAssign> GetVehicleAssignDetails(int vehicleAssignId);
        Task<VehicleAssign> AddVehicleAssign(VehicleAssign vehicleAssign);
        Task UpdateVehicleAssign(VehicleAssign vehicleAssign);
        Task DeleteVehicleAssign(int vehicleAssignId);
    }
}

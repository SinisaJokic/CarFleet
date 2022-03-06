using CarFleetModels;

namespace CarFleetUI.Services
{
    public interface IDriverService
    {
        Task<List<Driver>> GetAllDriver();
        Task<Driver> GetDriverDetails(int driverId);
        Task<Driver> AddDriver(Driver driver);
        Task UpdateDriver(Driver driver);
        Task DeleteDriver(int driverId);
    }
}

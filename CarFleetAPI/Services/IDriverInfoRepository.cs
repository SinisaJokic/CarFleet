using CarFleetAPI.Entities;

namespace CarFleetAPI.Services
{
    public interface IDriverInfoRepository
    {
        Task<IEnumerable<Driver>> GetDriversAsync();
        Task<Driver?> GetDriverAsync(int driverId);
        Task<bool> DriverExistsAsync(int driverId);
        //Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);
        //Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId,
        //    int pointOfInterestId);
        Task AddDriverAsync(Driver driver);
        void DeleteDriver(Driver driver);
        Task<bool> SaveChangesAsync();
    }
}

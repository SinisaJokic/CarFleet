using CarFleetAPI.Entities;
using CarFleetAPI.Models;

namespace CarFleetAPI.Services
{
    public interface IUserModelInfoRepository
    {
        Task<IEnumerable<UserModel>> GetUserModelsAsync();
        Task<UserModel?> GetUserModelAsync(int pkUser);
        Task<bool> UserModelExistsAsync(int pkUser);
        //Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);
        //Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId,
        //    int pointOfInterestId);
        Task AddUserModelAsync(UserModel userModel);
        void DeleteUserModel(UserModel userModel);
        Task<bool> SaveChangesAsync();

        Task<UserModel?> GetUserPassAsync(string userName,string password);
    }
}

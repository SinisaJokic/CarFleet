using CarFleetModels;

namespace CarFleetUI.Services
{
    public interface IUserService
    {
        Task<List<UserModel>> GetAllUser();
        Task<UserModel> GetUserDetails(int pkUser);
        Task<UserModel> AddUser(UserModel user);
        Task UpdateUser(UserModel user);
        Task DeleteUser(int pkUser);
    }
}

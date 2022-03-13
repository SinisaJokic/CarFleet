using CarFleetAPI.Models;
using CarFleetModels;

namespace CarFleetUI.Services
{
    public interface IAuthenticationService
    {
        UserModel User { get; }
        //User UserTemp { get; }
        Task Initialize();
        Task Login(string username, string password);
        Task Logout();
    }
}

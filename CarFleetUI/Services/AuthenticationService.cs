using Blazored.LocalStorage;
using CarFleetAPI.Models;
using CarFleetModels;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarFleetUI.Services
{
   
    public class AuthenticationService : IAuthenticationService
    {
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private IHttpService _httpService;
        //private readonly HttpClient _httpClient;

        public UserModel User { get; set; }

        //public User UserTemp { get; set; }

        public AuthenticationService(
            //HttpClient http,
            IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService
        ) {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }

       

        public async Task Initialize()
        {
            User = await _localStorageService.GetItemAsync<UserModel>("user");
        }

        public async Task Login(string username, string password)
        {
            User = await _httpService.Post<UserModel>("https://localhost:7228/api/authentication", new { username, password });
            await _localStorageService.SetItemAsync("user", User);
        }
        //public async Task<UserModel> Login(string username, string password)
        //{
        //    LoginModel loginModel = new LoginModel();
        //    loginModel.UserName=username;
        //    loginModel.Password=password;

        //    var loginModelJson =
        //        new StringContent(JsonSerializer.Serialize(loginModel), Encoding.UTF8, "application/json");

        //    var response = await _httpClient.PostAsync("api/authentication", loginModelJson);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        //var UserTemp2 = await response.Content.ReadAsStreamAsync();
        //        var UserTemp2 = await response.Content.ReadFromJsonAsync<UserModel>();
        //        //return  await JsonSerializer.DeserializeAsync<UserModel>(UserTemp2);
        //        return null;
        //    }

        //    return null;

        //}

        public async Task Logout()
        {
            User = null;
            await _localStorageService.RemoveItemAsync("user");
            _navigationManager.NavigateTo("login");
        }
    }
}
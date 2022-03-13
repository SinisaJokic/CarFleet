using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using CarFleetAPI.Models;
using CarFleetModels;

namespace CarFleetUI.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private IHttpService _httpService;

        public UserService(HttpClient http, IHttpService httpService)
        {
            _httpClient = http;
            _httpService = httpService;
        }
        

        public async Task<List<UserModel>> GetAllUser()
        {
            //var vehicles = await _httpClient.GetFromJsonAsync<List<Vehicle>>($"api/vehicle");
            //return (await _httpClient.GetFromJsonAsync<List<UserModel>>($"api/user"));  remirano zadnje

            return await _httpService.GetAll<UserModel>("https://localhost:7228/api/user");

            //var vehicles = await _httpClient.GetStreamAsync($"api/vehicle");



            //return await JsonSerializer.DeserializeAsync<IEnumerable<Vehicle>>
            //    (await _httpClient.GetStreamAsync($"api/vehicle"), new JsonSerializerOptions() { PropertyNameCaseInsensitive=true});

            //    return await JsonSerializer.DeserializeAsync<IEnumerable<Employee>>
            //        (await _httpClient.GetStreamAsync($"api/employee"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            //
        }
        public async Task<UserModel> GetUserDetails(int pkUser)
        {
            return await JsonSerializer.DeserializeAsync<UserModel>
                (await _httpClient.GetStreamAsync($"api/user/{pkUser}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<UserModel> AddUser(UserModel userModel)
        {
            var userModelJson =
                new StringContent(JsonSerializer.Serialize(userModel), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/user", userModelJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<UserModel>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateUser(UserModel userModel)
        {
            var userModelJson =
                new StringContent(JsonSerializer.Serialize(userModel), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/user", userModelJson);
        }

        public async Task DeleteUser(int pkUser)
        {
            await _httpClient.DeleteAsync($"api/driver/{pkUser}");
        }
    }
}

using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Blazored.LocalStorage;
using CarFleetModels;

namespace CarFleetUI.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly HttpClient _httpClient;
        private IHttpService _httpService;
        public UserModel User { get; set; }
        private ILocalStorageService _localStorageService;

        public VehicleService(HttpClient http, IHttpService httpService, 
            ILocalStorageService localStorageService)
        {
            _httpClient = http;
            _httpService = httpService;
            _localStorageService = localStorageService;
        }

        public async Task Initialize()
        {
            User = await _localStorageService.GetItemAsync<UserModel>("user");
        }


        public async Task<List<Vehicle>> GetAllVehicle()  // List
        {
            //var vehicles = await _httpClient.GetFromJsonAsync<List<Vehicle>>($"api/vehicle");
            //return (await _httpClient.GetFromJsonAsync<List<Vehicle>>($"api/vehicle"));   remirano zadnje

            return await _httpService.GetAll<Vehicle>("https://localhost:7228/api/vehicle");


            //var vehicles = await _httpClient.GetStreamAsync($"api/vehicle");



            //return await JsonSerializer.DeserializeAsync<IEnumerable<Vehicle>>
            //    (await _httpClient.GetStreamAsync($"api/vehicle"), new JsonSerializerOptions() { PropertyNameCaseInsensitive=true});

            //    return await JsonSerializer.DeserializeAsync<IEnumerable<Employee>>
            //        (await _httpClient.GetStreamAsync($"api/employee"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            //
        }
    public async Task<Vehicle> GetVehicleDetails(int vehicleId)
        {
            //return await JsonSerializer.DeserializeAsync<Vehicle>
            //    (await _httpClient.GetStreamAsync($"api/vehicle/{vehicleId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return await _httpService.Get<Vehicle>($"https://localhost:7228/api/vehicle/{vehicleId}");
        }

        public async Task<Vehicle> AddVehicle(Vehicle vehicle)
        {
            //var vehicleJson =
            //    new StringContent(JsonSerializer.Serialize(vehicle), Encoding.UTF8, "application/json");

            //var response = await _httpClient.PostAsync("api/vehicle", vehicleJson);

            //if (response.IsSuccessStatusCode)
            //{
            //    return await JsonSerializer.DeserializeAsync<Vehicle>(await response.Content.ReadAsStreamAsync());
            //}

            //return null;

            return await _httpService.Post<Vehicle>("https://localhost:7228/api/vehicle", vehicle);

        }

        public async Task UpdateVehicle(Vehicle vehicle)
        {
            //var vehicleJson =
            //    new StringContent(JsonSerializer.Serialize(vehicle), Encoding.UTF8, "application/json");

            //await _httpClient.PutAsync("api/vehicle", vehicleJson);

            int vehicleId=vehicle.Id;

            await _httpService.Put($"https://localhost:7228/api/vehicle/{vehicleId}", vehicle);
        }

        public async Task DeleteVehicle(int vehicleId)
        {
            await _httpClient.DeleteAsync($"api/vehicle/{vehicleId}");
        }
    }
}

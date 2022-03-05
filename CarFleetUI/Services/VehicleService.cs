using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using CarFleetModels;

namespace CarFleetUI.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly HttpClient _httpClient;

        public VehicleService(HttpClient http)
        {
            _httpClient = http;
        }
        

        public async Task<List<Vehicle>> GetAllVehicle()
        {
            //var vehicles = await _httpClient.GetFromJsonAsync<List<Vehicle>>($"api/vehicle");
            return (await _httpClient.GetFromJsonAsync<List<Vehicle>>($"api/vehicle"));

            //var vehicles = await _httpClient.GetStreamAsync($"api/vehicle");



            //return await JsonSerializer.DeserializeAsync<IEnumerable<Vehicle>>
            //    (await _httpClient.GetStreamAsync($"api/vehicle"), new JsonSerializerOptions() { PropertyNameCaseInsensitive=true});

        //    return await JsonSerializer.DeserializeAsync<IEnumerable<Employee>>
        //        (await _httpClient.GetStreamAsync($"api/employee"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        //
       }
        public async Task<Vehicle> GetVehicleDetails(int vehicleId)
        {
            return await JsonSerializer.DeserializeAsync<Vehicle>
                (await _httpClient.GetStreamAsync($"api/vehicle/{vehicleId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Vehicle> AddVehicle(Vehicle vehicle)
        {
            var vehicleJson =
                new StringContent(JsonSerializer.Serialize(vehicle), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/vehicle", vehicleJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Vehicle>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateVehicle(Vehicle vehicle)
        {
            var vehicleJson =
                new StringContent(JsonSerializer.Serialize(vehicle), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/vehicle", vehicleJson);
        }

        public async Task DeleteVehicle(int vehicleId)
        {
            await _httpClient.DeleteAsync($"api/vehicle/{vehicleId}");
        }
    }
}

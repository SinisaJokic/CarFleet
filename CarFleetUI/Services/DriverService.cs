using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using CarFleetModels;

namespace CarFleetUI.Services
{
    public class DriverService : IDriverService
    {
        private readonly HttpClient _httpClient;

        public DriverService(HttpClient http)
        {
            _httpClient = http;
        }
        

        public async Task<List<Driver>> GetAllDriver()
        {
            //var vehicles = await _httpClient.GetFromJsonAsync<List<Vehicle>>($"api/vehicle");
            return (await _httpClient.GetFromJsonAsync<List<Driver>>($"api/driver"));

            //var vehicles = await _httpClient.GetStreamAsync($"api/vehicle");



            //return await JsonSerializer.DeserializeAsync<IEnumerable<Vehicle>>
            //    (await _httpClient.GetStreamAsync($"api/vehicle"), new JsonSerializerOptions() { PropertyNameCaseInsensitive=true});

        //    return await JsonSerializer.DeserializeAsync<IEnumerable<Employee>>
        //        (await _httpClient.GetStreamAsync($"api/employee"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        //
       }
        public async Task<Driver> GetDriverDetails(int driverId)
        {
            return await JsonSerializer.DeserializeAsync<Driver>
                (await _httpClient.GetStreamAsync($"api/driver/{driverId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Driver> AddDriver(Driver driver)
        {
            var driverJson =
                new StringContent(JsonSerializer.Serialize(driver), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/driver", driverJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Driver>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateDriver(Driver driver)
        {
            var driverJson =
                new StringContent(JsonSerializer.Serialize(driver), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/driver", driverJson);
        }

        public async Task DeleteDriver(int driverId)
        {
            await _httpClient.DeleteAsync($"api/driver/{driverId}");
        }
    }
}

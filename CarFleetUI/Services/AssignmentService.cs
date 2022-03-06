﻿using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using CarFleetModels;

namespace CarFleetUI.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly HttpClient _httpClient;

        public AssignmentService(HttpClient http)
        {
            _httpClient = http;
        }
        

        public async Task<List<VehicleAssign>> GetAllVehicleAssign()
        {
            //var vehicles = await _httpClient.GetFromJsonAsync<List<Vehicle>>($"api/vehicle");
            return (await _httpClient.GetFromJsonAsync<List<VehicleAssign>>($"api/vehicleassign"));

            //var vehicles = await _httpClient.GetStreamAsync($"api/vehicle");



            //return await JsonSerializer.DeserializeAsync<IEnumerable<Vehicle>>
            //    (await _httpClient.GetStreamAsync($"api/vehicle"), new JsonSerializerOptions() { PropertyNameCaseInsensitive=true});

        //    return await JsonSerializer.DeserializeAsync<IEnumerable<Employee>>
        //        (await _httpClient.GetStreamAsync($"api/employee"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        //
       }
        public async Task<VehicleAssign> GetVehicleAssignDetails(int vehicleassignId)
        {
            return await JsonSerializer.DeserializeAsync<VehicleAssign>
                (await _httpClient.GetStreamAsync($"api/vehicleassign/{vehicleassignId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<VehicleAssign> AddVehicleAssign(VehicleAssign vehicleAssign)
        {
            var vehicleAssignJson =
                new StringContent(JsonSerializer.Serialize(vehicleAssign), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/vehicleassign", vehicleAssignJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<VehicleAssign>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateVehicleAssign(VehicleAssign vehicleAssign)
        {
            var vehicleAssignJson =
                new StringContent(JsonSerializer.Serialize(vehicleAssign), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/vehicleassign", vehicleAssignJson);
        }

        public async Task DeleteVehicleAssign(int vehicleAssignId)
        {
            await _httpClient.DeleteAsync($"api/vehicleassign/{vehicleAssignId}");
        }
    }
}
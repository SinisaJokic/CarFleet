using CarFleetModels;
using CarFleetUI.Services;
using Microsoft.AspNetCore.Components;

namespace CarFleetUI.Pages
{
    public class VehicleOverviewBase : ComponentBase
    {
        [Inject]
        public IVehicleService VehicleService { get; set; }

        public List<Vehicle> Vehicles { get; set; }
        public string Message { get; set; }


        protected override async Task OnInitializedAsync()
        {
            try
            {
                Vehicles = (await VehicleService.GetAllVehicle()).ToList();
                
            }
            catch (Exception e)
            {
                Message = "Something went wrong.";
            }

        }
    }
}

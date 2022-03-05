using CarFleetModels;
using CarFleetUI.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
//@inject IJSRuntime JS

namespace CarFleetUI.Pages
{
    public class MapBase : ComponentBase
    {
        [Inject]
        public IVehicleService VehicleService { get; set; }
        [Inject]
        public IJSRuntime JS { get; set; }

        [Parameter]
        public string VehicleId { get; set; }

        public Vehicle Vehicle { get; set; } = new Vehicle();
     
        //protected override async Task OnInitializedAsync()
        //{
        //    int.TryParse(VehicleId, out var vehicleId);

        //    if (vehicleId != 0) //new employee is being created
        //    {
        //        Vehicle = await VehicleService.GetVehicleDetails(int.Parse(VehicleId));
        //    }
        //}
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            int.TryParse(VehicleId, out var vehicleId);

            if (vehicleId != 0) //new employee is being created
            {
                Vehicle = await VehicleService.GetVehicleDetails(int.Parse(VehicleId));
            }

            if (firstRender)
            {
                await JS.InvokeVoidAsync("initializeMap", Vehicle.CurrentX, Vehicle.CurrentY);

                StateHasChanged();
            }

        }

    }
}

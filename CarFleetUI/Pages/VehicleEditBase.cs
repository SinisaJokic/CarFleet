using CarFleetModels;
using CarFleetUI.Services;
using Microsoft.AspNetCore.Components;

namespace CarFleetUI.Pages
{
    public class VehicleEditBase : ComponentBase
    {
        [Inject]
        public IVehicleService VehicleService { get; set; }
        [Parameter]
        public string VehicleId { get; set; }

        public Vehicle Vehicle { get; set; } = new Vehicle();
        public string Message { get; set; }
        protected string StatusClass = string.Empty;
        protected bool Saved;


        protected override async Task OnInitializedAsync()
        {
            Saved = false;

            int.TryParse(VehicleId, out var vehicleId);

            if (vehicleId != 0) //new employee is being created
            {
                Vehicle = await VehicleService.GetVehicleDetails(int.Parse(VehicleId));
            }
        }
        protected async Task HandleValidSubmit()
        {
            if (Vehicle.Id == 0) //new
            {
                var addedEmployee = await VehicleService.AddVehicle(Vehicle);

                if (addedEmployee != null)
                {
                    StatusClass = "alert-success";
                    Message = "New vehicle added successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new vehicle. Please try again.";
                    Saved = false;
                }
            }
            else
            {
                await VehicleService.UpdateVehicle(Vehicle);
                StatusClass = "alert-success";
                Message = "Vehicle updated successfully.";
                Saved = true;
            }
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }

        protected async Task DeleteVehicle()
        {
            await VehicleService.DeleteVehicle(Vehicle.Id);

            StatusClass = "alert-success";
            Message = "Deleted successfully";

            Saved = true;
        }
        //protected void NavigateToOverview()
        //{
        //    NavigationManager.NavigateTo("/vehicleoverview");
        //}
    }
}

using CarFleetModels;
using CarFleetUI.Services;
using Microsoft.AspNetCore.Components;

namespace CarFleetUI.Pages
{
    public class DriverEditBase : ComponentBase
    {
        [Inject]
        public IDriverService DriverService { get; set; }
        [Parameter]
        public string DriverId { get; set; }

        public Driver Driver { get; set; } = new Driver();
        public string Message { get; set; }
        protected string StatusClass = string.Empty;
        protected bool Saved;


        protected override async Task OnInitializedAsync()
        {
            Saved = false;

            int.TryParse(DriverId, out var driverId);

            if (driverId != 0) //new employee is being created
            {
                Driver = await DriverService.GetDriverDetails(int.Parse(DriverId));
            }
        }
        protected async Task HandleValidSubmit()
        {
            if (Driver.Id == 0) //new
            {
                var addedDriver = await DriverService.AddDriver(Driver);

                if (addedDriver != null)
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
                await DriverService.UpdateDriver(Driver);
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

        protected async Task DeleteDriver()
        {
            await DriverService.DeleteDriver(Driver.Id);

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

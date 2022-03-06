using CarFleetModels;
using CarFleetUI.Services;
using Microsoft.AspNetCore.Components;

namespace CarFleetUI.Pages
{
    public class DriverOverviewBase : ComponentBase
    {
        [Inject]
        public IDriverService DriverService { get; set; }

        public List<Driver> Drivers { get; set; }
        public string Message { get; set; }


        protected override async Task OnInitializedAsync()
        {
            try
            {
                Drivers = (await DriverService.GetAllDriver()).ToList();
                
            }
            catch (Exception e)
            {
                Message = "Something went wrong.";
            }

        }
    }
}

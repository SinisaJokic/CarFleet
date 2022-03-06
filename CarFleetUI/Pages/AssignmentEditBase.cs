using CarFleetModels;
using CarFleetUI.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace CarFleetUI.Pages
{
    public class AssignmentEditBase : ComponentBase
    {
        [Inject]
        public IAssignmentService AssignmentService { get; set; }

        [Inject]
        public IVehicleService VehicleService { get; set; }
        [Inject]
        public IDriverService DriverService { get; set; }

        //[Parameter]
        //public int VehicleAssignId { get; set; }

        public VehicleAssign VehicleAssign { get; set; } = new VehicleAssign
        {
            VehicleId = 1,
            FromDate = DateTime.Now,
        };
        public string Message { get; set; }
        protected string StatusClass = string.Empty;
        protected bool Saved;

        protected string Vehicle = string.Empty;

        public List<Vehicle> AllVehicle { get; set; } = new List<Vehicle>();
        public List<Driver> AllDriver { get; set; } = new List<Driver>();

        protected override async Task OnInitializedAsync()
        {
            //VehicleAssign = await AssignmentService.GetVehicleAssignDetails(VehicleAssignId);

            AllVehicle = (await VehicleService.GetAllVehicle()).ToList();
            AllDriver = (await DriverService.GetAllDriver()).ToList();
        }
        protected async Task HandleValidSubmit()
        {
            if (VehicleAssign.Id == 0) //new
            {
                var addedVehicleAssign = await AssignmentService.AddVehicleAssign(VehicleAssign);

                if (addedVehicleAssign != null)
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
                await AssignmentService.UpdateVehicleAssign(VehicleAssign);
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
    }
}

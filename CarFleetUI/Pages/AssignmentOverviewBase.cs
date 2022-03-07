using CarFleetModels;
using CarFleetUI.Services;
using Microsoft.AspNetCore.Components;

namespace CarFleetUI.Pages
{
    public class AssignmentOverviewBase : ComponentBase
    {
        [Inject]
        public IAssignmentService AssignmentService { get; set; }

        public List<VehicleAssign> VehicleAssigns { get; set; }
        public string Message { get; set; }


        protected override async Task OnInitializedAsync()
        {
            try
            {
                VehicleAssigns = (await AssignmentService.GetAllVehicleAssign()).ToList();
                
            }
            catch (Exception e)
            {
                Message = "Something went wrong.";
            }

        }
    }
}

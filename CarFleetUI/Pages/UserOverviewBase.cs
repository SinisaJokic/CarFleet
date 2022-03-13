using CarFleetModels;
using CarFleetUI.Services;
using Microsoft.AspNetCore.Components;

namespace CarFleetUI.Pages
{
    public class UserOverviewBase : ComponentBase
    {
        [Inject]
        public IUserService UserService { get; set; }

        public List<UserModel> Users { get; set; }
        public string Message { get; set; }


        protected override async Task OnInitializedAsync()
        {
            try
            {
                Users = (await UserService.GetAllUser()).ToList();
                
            }
            catch (Exception e)
            {
                Message = "Something went wrong.";
            }

        }
    }
}
